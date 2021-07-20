<System.Runtime.InteropServices.ComVisible(False)> _
Public Class ImageControl

    Private m_ScrollVisible As Boolean = True

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Public Properties"

    Public Sub deleteSelectedLine()
        DrawingBoard1.deleteSelectedLine()
    End Sub

    Public Sub setUnitsText(ByVal text As String)
        DrawingBoard1.setUnitsText(text)
    End Sub

    Public Sub setAllLinesPen(ByVal pen As Pen)
        DrawingBoard1.setAllLinesPen(pen)
    End Sub

    Public Sub setAllTextSize(ByVal size As Integer)
        DrawingBoard1.setAllTextSize(size)
    End Sub

    Public Sub addUnitScaleLine()
        DrawingBoard1.addUnitScaleLine()
    End Sub

    Public Sub nextTextPosition()
        DrawingBoard1.nextTextPosition()
    End Sub

    Public Property Mode() As DrawingBoard.ModeTypes
        Get
            Return DrawingBoard1.Mode
        End Get
        Set(ByVal value As DrawingBoard.ModeTypes)
            DrawingBoard1.Mode = value
        End Set
    End Property

    'Public Property PanButton() As System.Windows.Forms.MouseButtons
    '    Get
    '        Return DrawingBoard1.PanButton
    '    End Get
    '    Set(ByVal value As System.Windows.Forms.MouseButtons)
    '        DrawingBoard1.PanButton = value
    '    End Set
    'End Property

    Public Property ZoomOnMouseWheel() As Boolean
        Get
            Return DrawingBoard1.ZoomOnMouseWheel
        End Get
        Set(ByVal value As Boolean)
            DrawingBoard1.ZoomOnMouseWheel = value
        End Set
    End Property

    Public Property ZoomFactor() As Double
        Get
            Return DrawingBoard1.ZoomFactor
        End Get
        Set(ByVal value As Double)
            DrawingBoard1.ZoomFactor = value
        End Set
    End Property

    Public Property Origin() As System.Drawing.Point
        Get
            Return DrawingBoard1.Origin
        End Get
        Set(ByVal value As System.Drawing.Point)
            DrawingBoard1.Origin = value
        End Set
    End Property


    Public ReadOnly Property ApparentImageSize() As System.Drawing.Size
        Get
            Return DrawingBoard1.ApparentImageSize
        End Get
    End Property

    Public Sub fittoscreen()
        Me.DrawingBoard1.fitToScreen()
    End Sub

    Public Sub ZoomIn()
        Me.DrawingBoard1.ZoomIn()
    End Sub

    Public Sub ZoomOut()
        Me.DrawingBoard1.ZoomOut()
    End Sub

    Public Property ScrollbarsVisible() As Boolean
        Get
            Return m_ScrollVisible
        End Get
        Set(ByVal value As Boolean)
            m_ScrollVisible = value
            Me.HScrollBar1.Visible = value
            Me.VScrollBar1.Visible = value
            If value = False Then
                Me.DrawingBoard1.Dock = DockStyle.Fill
            Else
                Me.DrawingBoard1.Dock = DockStyle.None
                Me.DrawingBoard1.Location = New Point(0, 0)
                Me.DrawingBoard1.Width = ClientSize.Width - VScrollBar1.Width
                Me.DrawingBoard1.Height = ClientSize.Height - HScrollBar1.Height

            End If
        End Set
    End Property

#End Region

#Region "Public/Private Shadows"
    Public Shadows Property Image() As System.Drawing.Image
        Get
            If DesignMode then Return New Bitmap(10,10)
            Return DrawingBoard1.getRenderedImage()
        End Get
        Set(ByVal Value As System.Drawing.Image)
            DrawingBoard1.Image = Value
            If Value Is Nothing Then
                HScrollBar1.Enabled = False
                VScrollBar1.Enabled = False
                Exit Property
            End If
        End Set
    End Property

    Public Shadows Property initialimage() As System.Drawing.Image
        Get
            Return DrawingBoard1.initialImage
        End Get
        Set(ByVal value As System.Drawing.Image)
            DrawingBoard1.initialImage = value
            If value Is Nothing Then
                HScrollBar1.Enabled = False
                VScrollBar1.Enabled = False
                Exit Property
            End If
        End Set
    End Property

    Public Shadows Property BackgroundImage() As System.Drawing.Image
        Get
            Return DrawingBoard1.BackgroundImage
        End Get
        Set(ByVal Value As System.Drawing.Image)
            DrawingBoard1.BackgroundImage = Value
            If Value Is Nothing Then
                HScrollBar1.Enabled = False
                VScrollBar1.Enabled = False
                Exit Property
            End If
        End Set
    End Property

#End Region

    Public Sub RotateFlip(ByVal RotateFlipType As System.Drawing.RotateFlipType)
        DrawingBoard1.RotateFlip(RotateFlipType)
    End Sub

    Public Sub setPixelsPerUnit(ByVal pixelsPerUnit As Double)
        DrawingBoard1.setPixelsPerUnit(pixelsPerUnit)
    End Sub

    Public Function getPixelLengthOfSelectedLine() As Double
        Return DrawingBoard1.getPixelLengthOfSelectedLine()
    End Function

    Private Sub DrawingBoard1_SetScrollPositions() Handles DrawingBoard1.SetScrollPositions

        Dim DrawingWidth As Integer = DrawingBoard1.Image.Width
        Dim DrawingHeight As Integer = DrawingBoard1.Image.Height
        Dim OriginX As Integer = DrawingBoard1.Origin.X
        Dim OriginY As Integer = DrawingBoard1.Origin.Y
        Dim FactoredCtrlWidth As Integer = DrawingBoard1.Width / DrawingBoard1.ZoomFactor
        Dim FactoredCtrlHeight As Integer = DrawingBoard1.Height / DrawingBoard1.ZoomFactor
        HScrollBar1.Maximum = Me.DrawingBoard1.Image.Width
        VScrollBar1.Maximum = Me.DrawingBoard1.Image.Height

        If FactoredCtrlWidth >= DrawingBoard1.Image.Width Then
            HScrollBar1.Enabled = False
            HScrollBar1.Value = 0
        Else
            HScrollBar1.LargeChange = FactoredCtrlWidth
            HScrollBar1.Enabled = True
            HScrollBar1.Value = OriginX
        End If

        If FactoredCtrlHeight >= DrawingBoard1.Image.Height Then
            VScrollBar1.Enabled = False
            VScrollBar1.Value = 0
        Else
            VScrollBar1.Enabled = True
            VScrollBar1.LargeChange = FactoredCtrlHeight
            VScrollBar1.Value = OriginY
        End If

    End Sub


    Private Sub ScrollBar_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HScrollBar1.ValueChanged, VScrollBar1.ValueChanged
        Me.DrawingBoard1.Origin = New Point(HScrollBar1.Value, VScrollBar1.Value)
    End Sub

End Class
