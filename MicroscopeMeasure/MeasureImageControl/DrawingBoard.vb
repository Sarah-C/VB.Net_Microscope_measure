Imports System.Drawing.Imaging

<System.Runtime.InteropServices.ComVisible(False)> _
Public Class DrawingBoard

    Public Enum ModeTypes
        Pan
        Zoom
        Line
        Edit
    End Enum

    'Public Events
    Public Event SetScrollPositions()

    'Member Variables
    Private m_MouseButtons As System.Windows.Forms.MouseButtons = Windows.Forms.MouseButtons.Left
    Private m_WorkingImage As System.Drawing.Bitmap
    Private m_ReadOnlyImage As System.Drawing.Bitmap

    Private m_StartPoint As System.Drawing.Point
    Private m_EndPoint As System.Drawing.Point
    Private m_Origin As New System.Drawing.Point(0, 0)
    Private m_newLine As line = Nothing

    Private g As Graphics
    Private SrcRect As System.Drawing.Rectangle
    Private DestRect As System.Drawing.Rectangle

    Private m_ZoomOnMouseWheel As Boolean = True
    Private m_ZoomFactor As Double = 1.0

    Private m_ApparentImageSize As New Size(0, 0)

    Private m_DrawWidth As Integer
    Private m_DrawHeight As Integer

    Private m_centerpoint As Point
    Private m_dragging As Boolean = False
    Private m_OverEditLine As line = Nothing
    Private hoveringOverLine As line = Nothing

    Private m_UserMode As ModeTypes = ModeTypes.Pan

    Private m_Select_Rect As Rectangle
    Private m_Select_Pen As New Pen(Color.Red, 2) ' Pen used to indicate a selection on the image (zoom window)

    Private displaySettings As New DisplaySettings()
    Private measurements As New Measurements(displaySettings)

    Public Function getPixelLengthOfSelectedLine() As Double
        Dim line As line = displaySettings.focussedLine
        If line Is Nothing Then Return 0
        Return line.length
    End Function

    Public Sub setPixelsPerUnit(ByVal pixelsPerUnit As Double)
        displaySettings.pixelsPerUnit = pixelsPerUnit
        Invalidate()
    End Sub

    Public Sub deleteSelectedLine()
        If displaySettings.focussedLine IsNot Nothing Then
            measurements.lines.Remove(displaySettings.focussedLine)
            Invalidate()
        End If
    End Sub

#Region "Public/Private Shadows"
    Public Shadows Property Image() As System.Drawing.Image
        Get
            Return m_WorkingImage
        End Get
        Set(ByVal Value As System.Drawing.Image)
            If m_WorkingImage IsNot Nothing Then
                m_WorkingImage.Dispose()
                m_ReadOnlyImage.Dispose()
                m_Select_Rect = Nothing
                m_Origin = New Point(0, 0)
                m_ApparentImageSize = New Size(0, 0)
                m_ZoomFactor = 1
                GC.Collect()
            End If

            If Value Is Nothing Then
                m_WorkingImage = Nothing
                m_ReadOnlyImage = Nothing
                Me.Invalidate()
                Exit Property
            End If

            Dim r As New Rectangle(0, 0, Value.Width, Value.Height)
            m_WorkingImage = New Bitmap(Value.Width, Value.Height, Imaging.PixelFormat.Format32bppPArgb)
            Graphics.FromImage(m_WorkingImage).DrawImage(Value, r)

            m_ReadOnlyImage = Value

            'Force a paint
            Me.Invalidate()
        End Set
    End Property

    Public Shadows Property initialImage() As System.Drawing.Image
        Get
            Return m_WorkingImage
        End Get
        Set(ByVal value As System.Drawing.Image)
            Me.Image = value
            Me.ZoomFactor = 1
        End Set
    End Property

    Public Shadows Property BackgroundImage() As System.Drawing.Image
        Get
            Return Nothing
        End Get
        Set(ByVal Value As System.Drawing.Image)
            Me.Image = Value
            Me.ZoomFactor = 1
        End Set
    End Property

#End Region

#Region "Protected Overrides"

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        DrawImage(e.Graphics)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        DestRect = New System.Drawing.Rectangle(0, 0, ClientSize.Width, ClientSize.Height)
        ComputeDrawingArea()
        MyBase.OnSizeChanged(e)
    End Sub

#End Region

#Region "Public Properties"

    Public Sub ZoomIn()
        ZoomImage(True)
    End Sub

    Public Sub ZoomOut()
        ZoomImage(False)
    End Sub

    Private Sub ZoomImage(ByVal ZoomIn As Boolean)
        Dim loc As Point = PointToClient(Cursor.Position)
        m_centerpoint.X = m_Origin.X + (SrcRect.Width / 2)
        m_centerpoint.Y = m_Origin.Y + (SrcRect.Height / 2)
        ZoomFactor = Math.Round(ZoomFactor * If(ZoomIn, 1.1, 0.9), 2)
        m_Origin.X = m_centerpoint.X - ClientSize.Width / m_ZoomFactor / 2
        m_Origin.Y = m_centerpoint.Y - ClientSize.Height / m_ZoomFactor / 2
        CheckBounds()
        displaySettings.origin = m_Origin
    End Sub

    Public Property PanButton() As System.Windows.Forms.MouseButtons
        Get
            Return m_MouseButtons
        End Get
        Set(ByVal value As System.Windows.Forms.MouseButtons)
            m_MouseButtons = value
        End Set
    End Property

    Public Property ZoomOnMouseWheel() As Boolean
        Get
            Return m_ZoomOnMouseWheel
        End Get
        Set(ByVal value As Boolean)
            m_ZoomOnMouseWheel = value
        End Set
    End Property

    Public Property ZoomFactor() As Double
        Get
            Return m_ZoomFactor
        End Get
        Set(ByVal value As Double)
            m_ZoomFactor = value
            If m_ZoomFactor > 15 Then m_ZoomFactor = 15
            If m_ZoomFactor < 0.05 Then m_ZoomFactor = 0.05
            If Not m_WorkingImage Is Nothing Then
                m_ApparentImageSize.Height = m_WorkingImage.Height * m_ZoomFactor
                m_ApparentImageSize.Width = m_WorkingImage.Width * m_ZoomFactor
                ComputeDrawingArea()
                CheckBounds()
            End If
            displaySettings.zoomFactor = m_ZoomFactor
            displaySettings.origin = m_Origin
            Me.Invalidate()
        End Set
    End Property

    Public Property Origin() As System.Drawing.Point
        Get
            Return m_Origin
        End Get
        Set(ByVal value As System.Drawing.Point)
            m_Origin = value
            displaySettings.origin = value
            Me.Invalidate()
        End Set
    End Property

    Public ReadOnly Property ApparentImageSize() As System.Drawing.Size
        Get
            Return m_ApparentImageSize
        End Get
    End Property

    Public Property Mode() As ModeTypes
        Get
            Return m_UserMode
        End Get
        Set(ByVal value As ModeTypes)
            m_UserMode = value
        End Set
    End Property

    Public Sub fitToScreen()
        Me.Origin = New Point(0, 0)
        If m_WorkingImage Is Nothing Then Exit Sub
        ZoomFactor = Math.Min(ClientSize.Width / m_WorkingImage.Width, ClientSize.Height / m_WorkingImage.Height)
    End Sub

    Public Function getRenderedImage() As Bitmap
        Dim FL As line = displaySettings.focussedLine
        Dim HOL As line = displaySettings.hoveringOverLine
        displaySettings.focussedLine = Nothing
        displaySettings.hoveringOverLine = Nothing
        Dim rect = New System.Drawing.Rectangle(0, 0, m_ReadOnlyImage.Width, m_ReadOnlyImage.Height)
        Graphics.FromImage(m_WorkingImage).DrawImage(m_ReadOnlyImage, rect)
        measurements.DrawAllMeasurementsAbsolute(Graphics.FromImage(m_WorkingImage))
        displaySettings.focussedLine = FL
        displaySettings.hoveringOverLine = HOL
        Return m_WorkingImage.Clone()
    End Function

#End Region

    Private Sub DrawImage(ByRef g As Graphics)
        If m_WorkingImage Is Nothing Then Exit Sub
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
        If ZoomFactor <= 2 Then
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias ' Line quality
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic ' Image stretching and shrinking
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality ' overwrite quality
        Else
            g.SmoothingMode = Drawing2D.SmoothingMode.None ' Line quality
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor ' Image stretching and shrinking
            g.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed ' overwrite quality
        End If
        SrcRect = New System.Drawing.Rectangle(m_Origin.X, m_Origin.Y, m_DrawWidth, m_DrawHeight)
        Graphics.FromImage(m_WorkingImage).DrawImage(m_ReadOnlyImage, SrcRect, SrcRect, GraphicsUnit.Pixel)
        measurements.DrawAllMeasurementsAbsolute(Graphics.FromImage(m_WorkingImage))
        g.DrawImage(m_WorkingImage, DestRect, SrcRect, GraphicsUnit.Pixel)
        If Mode = ModeTypes.Zoom Then g.DrawRectangle(m_Select_Pen, m_Select_Rect)
        If Mode = ModeTypes.Line Then g.DrawLine(m_Select_Pen, m_StartPoint, m_EndPoint)
        RaiseEvent SetScrollPositions()
    End Sub

    Private Sub ComputeDrawingArea()
        m_DrawHeight = Me.Height / m_ZoomFactor
        m_DrawWidth = Me.Width / m_ZoomFactor
    End Sub

    Public Sub addUnitScaleLine()
        Dim l As Double = displaySettings.pixelsPerUnit
        Dim x As Double = 20.0
        Dim y As Double = 50.0
        Dim newLine As line = measurements.addLineImageSpace(New Point(x, y), New Point(l + x, y))
        newLine.isAUnitScale = True
        Invalidate()
    End Sub

    Public Sub nextTextPosition()
        If displaySettings.focussedLine Is Nothing Then Exit Sub
        Select Case displaySettings.focussedLine.textPosition
            Case line.position.start : displaySettings.focussedLine.textPosition = line.position.middle
            Case line.position.middle : displaySettings.focussedLine.textPosition = line.position.end
            Case line.position.end : displaySettings.focussedLine.textPosition = line.position.start
        End Select
        Invalidate()
    End Sub

    Private Sub ImageViewer_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If m_WorkingImage Is Nothing Then Exit Sub
        m_dragging = True
        m_Select_Rect = Nothing
        m_StartPoint = New Point(e.X, e.Y)
        m_EndPoint = New Point(e.X, e.Y)
        If Mode = ModeTypes.Line Then
            m_OverEditLine = Nothing
            m_newLine = measurements.addLineScreenSpace(m_StartPoint, m_EndPoint)
            displaySettings.focussedLine = m_newLine
        End If
        If Mode <> ModeTypes.Pan And Mode <> ModeTypes.Zoom Then displaySettings.focussedLine = measurements.selectClosestLine(mouseOnImageLocation())
        Invalidate()
    End Sub

    Public Function mouseOnImageLocation() As PointF
        Dim m As PointF = PointToClient(Windows.Forms.Cursor.Position)
        m.X /= m_ZoomFactor
        m.Y /= m_ZoomFactor
        m.X += m_Origin.X
        m.Y += m_Origin.Y
        Return m
    End Function

    Private Sub ImageViewer_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If m_WorkingImage Is Nothing Then Exit Sub
        Dim dirty As Boolean = False

        If Mode = ModeTypes.Edit Then
            If Not m_dragging Then m_OverEditLine = measurements.selectClosestLineByEnd(mouseOnImageLocation())
            dirty = True
        End If

        If Mode = ModeTypes.Edit Then
            If Not m_dragging Then
                displaySettings.hoveringOverLine = measurements.selectClosestLine(mouseOnImageLocation())
                dirty = True
            End If
        End If

        If Not m_dragging Then
            If dirty Then Invalidate()
            Exit Sub
        End If

        If e.Button <> m_MouseButtons Then Exit Sub

        Dim DeltaX As Integer = m_StartPoint.X - e.X
        Dim DeltaY As Integer = m_StartPoint.Y - e.Y

        If (Mode = ModeTypes.Pan Or (Mode = ModeTypes.Edit And m_OverEditLine Is Nothing And displaySettings.focussedLine Is Nothing)) Then
            m_Origin.X = m_Origin.X + (DeltaX / m_ZoomFactor)
            m_Origin.Y = m_Origin.Y + (DeltaY / m_ZoomFactor)

            CheckBounds()

            m_StartPoint.X = e.X
            m_StartPoint.Y = e.Y
            displaySettings.origin = m_Origin

            dirty = True
        End If

        If Mode = ModeTypes.Edit And m_OverEditLine IsNot Nothing Then
            displaySettings.focussedLine = m_OverEditLine
            Dim sp As New PointF(0, 0)
            If m_OverEditLine.startIsSelected Then
                sp = m_OverEditLine.start
            Else
                sp = m_OverEditLine.end
            End If

            sp.X -= (DeltaX / m_ZoomFactor)
            sp.Y -= (DeltaY / m_ZoomFactor)

            If m_OverEditLine.startIsSelected Then
                m_OverEditLine.start = sp
            Else
                m_OverEditLine.end = sp
            End If
            'CheckBounds()
            m_StartPoint.X = e.X
            m_StartPoint.Y = e.Y
            dirty = True
        End If

        If Mode = ModeTypes.Edit And m_OverEditLine Is Nothing And displaySettings.focussedLine IsNot Nothing Then

            Dim sp As PointF = displaySettings.focussedLine.start
            Dim ep As PointF = displaySettings.focussedLine.end

            sp.X -= (DeltaX / m_ZoomFactor)
            sp.Y -= (DeltaY / m_ZoomFactor)
            ep.X -= (DeltaX / m_ZoomFactor)
            ep.Y -= (DeltaY / m_ZoomFactor)

            displaySettings.focussedLine.start = sp
            displaySettings.focussedLine.end = ep

            m_StartPoint.X = e.X
            m_StartPoint.Y = e.Y
            dirty = True
        End If

        If Mode = ModeTypes.Zoom Then
            If (New Rectangle(0, 0, Me.Width, Me.Height)).Contains(PointToClient(Windows.Forms.Cursor.Position)) Then
                Dim Width As Integer = System.Math.Abs(m_StartPoint.X - e.X)
                Dim Height As Integer = System.Math.Abs(m_StartPoint.Y - e.Y)
                Dim UpperLeft = New Point(System.Math.Min(m_StartPoint.X, e.X), System.Math.Min(m_StartPoint.Y, e.Y))
                m_Select_Rect = New Rectangle(UpperLeft.X, UpperLeft.Y, Width, Height)
                dirty = True
            End If
        End If

        If Mode = ModeTypes.Line Then
            If (New Rectangle(0, 0, Me.Width, Me.Height)).Contains(PointToClient(Windows.Forms.Cursor.Position)) Then
                m_EndPoint = New Point(e.X, e.Y)
                measurements.updateLineScreenSpace(m_newLine, m_StartPoint, m_EndPoint)
                dirty = True
            End If
        End If

        If dirty Then Invalidate()
    End Sub

    Private Sub CheckBounds()
        If m_WorkingImage Is Nothing Then Exit Sub

        If m_Origin.X < 0 Then m_Origin.X = 0
        If m_Origin.Y < 0 Then m_Origin.Y = 0
        If m_Origin.X > m_WorkingImage.Width - (ClientSize.Width / m_ZoomFactor) Then
            m_Origin.X = m_WorkingImage.Width - (ClientSize.Width / m_ZoomFactor)
        End If
        If m_Origin.Y > m_WorkingImage.Height - (ClientSize.Height / m_ZoomFactor) Then
            m_Origin.Y = m_WorkingImage.Height - (ClientSize.Height / m_ZoomFactor)
        End If

        If m_Origin.X < 0 Then m_Origin.X = 0
        If m_Origin.Y < 0 Then m_Origin.Y = 0

    End Sub

    Private Sub DrawingBoard_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        If m_WorkingImage Is Nothing Then Exit Sub
        If Not m_dragging Then Exit Sub
        m_dragging = False

        If Mode = ModeTypes.Zoom Then
            If m_Select_Rect = Nothing Then Exit Sub
            ZoomSelection()
        End If

        If Mode = ModeTypes.Line Then

            If m_StartPoint <> m_EndPoint Then
                'measurements.DrawMeasurement(Graphics.FromImage(m_OriginalImage), m_newLine)
                'measurements.updateLine(m_newLine, m_StartPoint, m_EndPoint)
            Else
                measurements.lines.Remove(m_newLine)
                displaySettings.focussedLine = Nothing
            End If

            m_StartPoint = Nothing
            m_EndPoint = Nothing
            Invalidate()
        End If

    End Sub

    Private Sub ZoomSelection()
        If m_WorkingImage Is Nothing Then Exit Sub
        Try
            Dim NewOrigin As New Point(CInt(Me.Origin.X + (m_Select_Rect.X / ZoomFactor)), _
                                              Me.Origin.Y + (m_Select_Rect.Y / ZoomFactor))

            Dim NewFactor As Double
            If m_Select_Rect.Width > m_Select_Rect.Height Then
                NewFactor = (ClientSize.Width / (m_Select_Rect.Width / ZoomFactor))
            Else
                NewFactor = (ClientSize.Height / (m_Select_Rect.Height / ZoomFactor))
            End If

            Me.Origin = NewOrigin
            Me.ZoomFactor = NewFactor

        Catch ex As Exception
            Throw ex
        End Try
        m_Select_Rect = Nothing
    End Sub

    Private Sub ImageViewer_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Not ZoomOnMouseWheel Or m_dragging Then Exit Sub
        If e.Delta > 0 Then
            ZoomImage(True)
        ElseIf e.Delta < 0 Then
            ZoomImage(False)
        End If
    End Sub

    Public Sub RotateFlip(ByVal RotateFlipType As System.Drawing.RotateFlipType)
        If m_WorkingImage Is Nothing Then Exit Sub
        m_WorkingImage.RotateFlip(RotateFlipType)
        Invalidate()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
    End Sub

    Private Sub DrawingBoard_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.ComputeDrawingArea()
    End Sub

    Public Sub setAllLinesPen(ByVal pen As Pen)
        For Each line As line In measurements.lines
            line.pen = pen
        Next
        displaySettings.linePen = pen
        Invalidate()
    End Sub

    Public Sub setAllTextSize(ByVal size As single)
        Dim f As New Font("Arial",size)
            For Each line As line In measurements.lines
            line.textStyle = f
        Next
        displaySettings.font = f
        Invalidate()
    End Sub

    Public Sub setUnitsText(ByVal text As String)
        displaySettings.units = text
        Invalidate()
    End Sub

    Private Sub aKeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back Then
            deleteSelectedLine()
        End If
    End Sub

End Class

