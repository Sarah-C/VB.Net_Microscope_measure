Imports System.IO
Imports System.Linq
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Public Class Form_Measurement

    Public scales As New SortedList(Of String, String)()

    Private Sub Form_Measurement_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Using file As New System.IO.StreamWriter("Calibration.txt")
            For Each a As KeyValuePair(Of String, String) In scales
                file.WriteLine(a.Key)
                file.WriteLine(a.Value)
            Next
        End Using
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ColorDialog1.Color = Color.Blue
        FillScaleComboBox()
        FillWidthComboBox()
        FillTextSizeComboBox()
        'Dim bmp As New Bitmap("landscape.jpg")
        'Me.ImageControl1.Image = bmp

        textbox_units.Text = " um"
        radiobutton_lineDraw.Select()
        combobox_penThickness.SelectedIndex = 1
        combobox_textSize.SelectedIndex = 2
    End Sub

    Public Sub FillWidthComboBox()
        Dim widths As New SortedList(Of String, String)
        widths.Add("1", "1")
        widths.Add("2", "2")
        widths.Add("3", "3")
        widths.Add("4", "4")
        widths.Add("5", "5")
        widths.Add("6", "6")
        widths.Add("7", "7")
        widths.Add("8", "8")
        widths.Add("9", "9")
        combobox_penThickness.DisplayMember = "Key"
        combobox_penThickness.ValueMember = "Value"
        combobox_penThickness.DataSource = New BindingSource(widths, Nothing)
    End Sub

    Public Sub FillTextSizeComboBox()
        Dim sizes As New dictionary(Of String, String)
        sizes.Add("xx - Small", "8")
        sizes.Add("x - Small", "10")
        sizes.Add("Small", "12")
        sizes.Add("Medium", "30")
        sizes.Add("Large", "45")
        sizes.Add("x - Large", "60")
        sizes.Add("xx - Large", "75")
        combobox_textSize.DisplayMember = "Key"
        combobox_textSize.ValueMember = "Value"
        combobox_textSize.DataSource = New BindingSource(sizes, Nothing)
    End Sub

    Public Sub FillScaleComboBox()
        If File.Exists("Calibration.txt") Then
            Dim key As String = Nothing
            Dim value As String = Nothing
            Using file As New System.IO.StreamReader("Calibration.txt")
                key = file.ReadLine()
                Do While key IsNot Nothing
                    value = file.ReadLine()
                    scales.Add(key, value)
                    key = file.ReadLine()
                Loop
            End Using
        Else
            scales.Add("x20 - 1600 x 1200", "200")
            scales.Add("x20 - 800 x 600", "250")
            scales.Add("x20 - 640 x 480", "300")
        End If
        combobox_scales.DisplayMember = "Key"
        combobox_scales.ValueMember = "Value"
        combobox_scales.DataSource = New BindingSource(scales, Nothing)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ImageControl1.ZoomIn()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ImageControl1.ZoomOut()
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) 
    '    Me.ImageControl1.RotateFlip(Me.combobox_flip.SelectedItem)
    'End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ImageControl1.Image = Nothing
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.ImageControl1.fittoscreen()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        With OpenFileDialog1
            Dim bmp As Bitmap
            .CheckFileExists = True
            .CheckPathExists = True
            .InitialDirectory = "c:\"
            .Multiselect = False
            .Filter = "*.* (Pick a valid image file)| *.*"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                bmp = New Bitmap(.FileName)
                Me.ImageControl1.Image = bmp
            End If
        End With
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radiobutton_panMode.CheckedChanged, radiobutton_windowZoom.CheckedChanged, radiobutton_lineDraw.CheckedChanged, radiobutton_lineEdit.CheckedChanged
        If radiobutton_panMode.Checked Then Me.ImageControl1.Mode = ImageControl.DrawingBoard.ModeTypes.Pan
        If radiobutton_windowZoom.Checked Then Me.ImageControl1.Mode = ImageControl.DrawingBoard.ModeTypes.Zoom
        If radiobutton_lineDraw.Checked Then Me.ImageControl1.Mode = ImageControl.DrawingBoard.ModeTypes.Line
        If radiobutton_lineEdit.Checked Then Me.ImageControl1.Mode = ImageControl.DrawingBoard.ModeTypes.Edit
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.ImageControl1.Origin = New Point(0, 0)
        Me.ImageControl1.ZoomFactor = 1
    End Sub

    Private Sub button_calibrate_Click(ByVal sender As System.Object, e As System.EventArgs) Handles button_calibrate.Click
        Dim lastLineLength As Double = ImageControl1.getPixelLengthOfSelectedLine()
        If lastLineLength = 0 Then
            MsgBox("You need to select a line to do calibration.")
            Exit Sub
        End If
        Dim data As String = Interaction.InputBox("How many units are in the line?", "Units in line", "1")
        If IsNumeric(data) Then
            Dim unitsInLine As Double = CDbl(data)
            Dim pixelsPerUnit As Double = lastLineLength / unitsInLine
            Dim se As KeyValuePair(Of String, String) = DirectCast(combobox_scales.SelectedItem, KeyValuePair(Of String, String))
            scales(se.Key) = pixelsPerUnit.ToString()
            combobox_scales.DataSource = New BindingSource(scales, Nothing)
        End If
    End Sub

    Private Sub button_newScale_Click(ByVal sender As System.Object, e As System.EventArgs) Handles button_newScale.Click
        Dim lastLineLength As Double = ImageControl1.getPixelLengthOfSelectedLine()
        If lastLineLength = 0 Then
            MsgBox("You need to select a line to do calibration.")
            Exit Sub
        End If
        Dim name As String = Interaction.InputBox("What do you want to name this resolution?", "Resolution name")
        If name = "" Then Exit Sub
        Dim data As String = Interaction.InputBox("How many units are in the line?", "Units in line", "1")
        If IsNumeric(data) Then
            Dim unitsInLine As Double = CDbl(data)
            Dim pixelsPerUnit As Double = lastLineLength / unitsInLine
            Dim se As KeyValuePair(Of String, String) = DirectCast(combobox_scales.SelectedItem, KeyValuePair(Of String, String))
            scales.Add(name, pixelsPerUnit.ToString())
            combobox_scales.DataSource = New BindingSource(scales, Nothing)
            combobox_scales.SelectedIndex = scales.IndexOfKey(name)
        End If
    End Sub

    Private Sub combobox_scales_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles combobox_scales.SelectedIndexChanged
        textbox_pixelsPerUnit.Text = combobox_scales.SelectedValue
        ImageControl1.setPixelsPerUnit(CDbl(combobox_scales.SelectedValue))
        ImageControl1.Invalidate()
    End Sub

    Private Sub button_addScale_Click(sender As System.Object, e As System.EventArgs) Handles button_addScale.Click
        ImageControl1.addUnitScaleLine()
    End Sub

    Private Sub button_textPosition_Click(sender As System.Object, e As System.EventArgs) Handles button_textPosition.Click
        ImageControl1.nextTextPosition()
    End Sub

    Private Sub button_deleteScale_Click(sender As System.Object, e As System.EventArgs) Handles button_deleteScale.Click
        If scales.Count > 1 Then
            scales.Remove(DirectCast(combobox_scales.SelectedItem, KeyValuePair(Of String, String)).Key)
            combobox_scales.DataSource = New BindingSource(scales, Nothing)
            combobox_scales.SelectedIndex = 0
        End If
    End Sub

    Private Sub button_saveImage_Click(sender As System.Object, e As System.EventArgs) Handles button_saveImage.Click
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "JPG Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png"
        saveFileDialog1.Title = "Save an Image File"
        saveFileDialog1.ShowDialog()
        If saveFileDialog1.FileName <> "" Then
            Dim codecInfo As ImageCodecInfo = GetEncoder(ImageFormat.Jpeg)
            Dim customEncoder As Encoder = Encoder.Quality
            Dim EncoderParameters As New EncoderParameters(1)
            Dim JPGEncoderParameter As New EncoderParameter(customEncoder, 90)
            EncoderParameters.Param(0) = JPGEncoderParameter
            Dim fileStream As FileStream = CType(saveFileDialog1.OpenFile(), FileStream)
            Select Case saveFileDialog1.FilterIndex
                Case 1 : ImageControl1.Image.Save(fileStream, codecInfo, EncoderParameters)
                Case 2 : ImageControl1.Image.Save(fileStream, ImageFormat.Bmp)
                Case 3 : ImageControl1.Image.Save(fileStream, ImageFormat.Gif)
                Case 4 : ImageControl1.Image.Save(fileStream, ImageFormat.Png)
            End Select
            fileStream.Close()
        End If
    End Sub

    Private Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo
        Dim codecs() As ImageCodecInfo = ImageCodecInfo.GetImageDecoders()
        For Each codec As ImageCodecInfo In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing
    End Function

    Private Sub textbox_units_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textbox_units.TextChanged
        ImageControl1.setUnitsText(textbox_units.Text)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles button_lineStyle.Click
        Dim result As DialogResult = ColorDialog1.ShowDialog()
        If result = DialogResult.OK Then updateLine(ColorDialog1.Color, combobox_penThickness.SelectedValue)
    End Sub

    Private Sub combobox_penThickness_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles combobox_penThickness.SelectedIndexChanged
        updateLine(ColorDialog1.Color, combobox_penThickness.SelectedValue)
    End Sub

    Private Sub updateLine(ByVal c As Color, ByVal w As Integer)
        Dim pen As New Pen(New SolidBrush(c), w)
        ImageControl1.setAllLinesPen(pen)
    End Sub

    Private Sub combobox_textSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles combobox_textSize.SelectedIndexChanged
        ImageControl1.setAllTextSize(CSng(combobox_textSize.SelectedValue))
    End Sub

End Class
