<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Measurement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Measurement))
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.radiobutton_panMode = New System.Windows.Forms.RadioButton()
        Me.radiobutton_windowZoom = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radiobutton_lineEdit = New System.Windows.Forms.RadioButton()
        Me.radiobutton_lineDraw = New System.Windows.Forms.RadioButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.button_calibrate = New System.Windows.Forms.Button()
        Me.combobox_scales = New System.Windows.Forms.ComboBox()
        Me.button_newScale = New System.Windows.Forms.Button()
        Me.textbox_pixelsPerUnit = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.button_addScale = New System.Windows.Forms.Button()
        Me.button_textPosition = New System.Windows.Forms.Button()
        Me.button_deleteScale = New System.Windows.Forms.Button()
        Me.button_saveImage = New System.Windows.Forms.Button()
        Me.textbox_units = New System.Windows.Forms.TextBox()
        Me.button_lineStyle = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.combobox_penThickness = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.combobox_textSize = New System.Windows.Forms.ComboBox()
        Me.ImageControl1 = New ImageControl.ImageControl()
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(209, 73)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 7
        Me.Button5.Text = "Fit Image"
        Me.Button5.UseVisualStyleBackColor = true
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(374, 12)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(77, 23)
        Me.Button6.TabIndex = 8
        Me.Button6.Text = "Load"
        Me.Button6.UseVisualStyleBackColor = true
        '
        'radiobutton_panMode
        '
        Me.radiobutton_panMode.AutoSize = true
        Me.radiobutton_panMode.Location = New System.Drawing.Point(16, 15)
        Me.radiobutton_panMode.Name = "radiobutton_panMode"
        Me.radiobutton_panMode.Size = New System.Drawing.Size(74, 17)
        Me.radiobutton_panMode.TabIndex = 10
        Me.radiobutton_panMode.TabStop = true
        Me.radiobutton_panMode.Text = "Pan Mode"
        Me.radiobutton_panMode.UseVisualStyleBackColor = true
        '
        'radiobutton_windowZoom
        '
        Me.radiobutton_windowZoom.AutoSize = true
        Me.radiobutton_windowZoom.Location = New System.Drawing.Point(96, 15)
        Me.radiobutton_windowZoom.Name = "radiobutton_windowZoom"
        Me.radiobutton_windowZoom.Size = New System.Drawing.Size(94, 17)
        Me.radiobutton_windowZoom.TabIndex = 11
        Me.radiobutton_windowZoom.TabStop = true
        Me.radiobutton_windowZoom.Text = "Window Zoom"
        Me.radiobutton_windowZoom.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radiobutton_lineEdit)
        Me.GroupBox1.Controls.Add(Me.radiobutton_lineDraw)
        Me.GroupBox1.Controls.Add(Me.radiobutton_panMode)
        Me.GroupBox1.Controls.Add(Me.radiobutton_windowZoom)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(356, 41)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Mode"
        '
        'radiobutton_lineEdit
        '
        Me.radiobutton_lineEdit.AutoSize = true
        Me.radiobutton_lineEdit.Location = New System.Drawing.Point(275, 15)
        Me.radiobutton_lineEdit.Name = "radiobutton_lineEdit"
        Me.radiobutton_lineEdit.Size = New System.Drawing.Size(66, 17)
        Me.radiobutton_lineEdit.TabIndex = 13
        Me.radiobutton_lineEdit.TabStop = true
        Me.radiobutton_lineEdit.Text = "Line Edit"
        Me.radiobutton_lineEdit.UseVisualStyleBackColor = true
        '
        'radiobutton_lineDraw
        '
        Me.radiobutton_lineDraw.AutoSize = true
        Me.radiobutton_lineDraw.Location = New System.Drawing.Point(196, 15)
        Me.radiobutton_lineDraw.Name = "radiobutton_lineDraw"
        Me.radiobutton_lineDraw.Size = New System.Drawing.Size(73, 17)
        Me.radiobutton_lineDraw.TabIndex = 12
        Me.radiobutton_lineDraw.TabStop = true
        Me.radiobutton_lineDraw.Text = "Line Draw"
        Me.radiobutton_lineDraw.UseVisualStyleBackColor = true
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(287, 73)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 23)
        Me.Button7.TabIndex = 7
        Me.Button7.Text = "Actual Pixels"
        Me.Button7.UseVisualStyleBackColor = true
        '
        'button_calibrate
        '
        Me.button_calibrate.Location = New System.Drawing.Point(599, 75)
        Me.button_calibrate.Name = "button_calibrate"
        Me.button_calibrate.Size = New System.Drawing.Size(83, 23)
        Me.button_calibrate.TabIndex = 13
        Me.button_calibrate.Text = "Re-scale"
        Me.button_calibrate.UseVisualStyleBackColor = true
        '
        'combobox_scales
        '
        Me.combobox_scales.FormattingEnabled = true
        Me.combobox_scales.Location = New System.Drawing.Point(239, 46)
        Me.combobox_scales.Name = "combobox_scales"
        Me.combobox_scales.Size = New System.Drawing.Size(327, 21)
        Me.combobox_scales.TabIndex = 14
        '
        'button_newScale
        '
        Me.button_newScale.Location = New System.Drawing.Point(688, 75)
        Me.button_newScale.Name = "button_newScale"
        Me.button_newScale.Size = New System.Drawing.Size(75, 23)
        Me.button_newScale.TabIndex = 15
        Me.button_newScale.Text = "New scale"
        Me.button_newScale.UseVisualStyleBackColor = true
        '
        'textbox_pixelsPerUnit
        '
        Me.textbox_pixelsPerUnit.Location = New System.Drawing.Point(98, 46)
        Me.textbox_pixelsPerUnit.Name = "textbox_pixelsPerUnit"
        Me.textbox_pixelsPerUnit.ReadOnly = true
        Me.textbox_pixelsPerUnit.Size = New System.Drawing.Size(135, 20)
        Me.textbox_pixelsPerUnit.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(13, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Pixels per Unit"
        '
        'button_addScale
        '
        Me.button_addScale.Location = New System.Drawing.Point(106, 73)
        Me.button_addScale.Name = "button_addScale"
        Me.button_addScale.Size = New System.Drawing.Size(85, 23)
        Me.button_addScale.TabIndex = 18
        Me.button_addScale.Text = "Add std. scale"
        Me.button_addScale.UseVisualStyleBackColor = true
        '
        'button_textPosition
        '
        Me.button_textPosition.Location = New System.Drawing.Point(12, 73)
        Me.button_textPosition.Name = "button_textPosition"
        Me.button_textPosition.Size = New System.Drawing.Size(75, 23)
        Me.button_textPosition.TabIndex = 19
        Me.button_textPosition.Text = "Label pos >"
        Me.button_textPosition.UseVisualStyleBackColor = true
        '
        'button_deleteScale
        '
        Me.button_deleteScale.Location = New System.Drawing.Point(688, 45)
        Me.button_deleteScale.Name = "button_deleteScale"
        Me.button_deleteScale.Size = New System.Drawing.Size(75, 23)
        Me.button_deleteScale.TabIndex = 20
        Me.button_deleteScale.Text = "Delete scale"
        Me.button_deleteScale.UseVisualStyleBackColor = true
        '
        'button_saveImage
        '
        Me.button_saveImage.Location = New System.Drawing.Point(457, 12)
        Me.button_saveImage.Name = "button_saveImage"
        Me.button_saveImage.Size = New System.Drawing.Size(78, 23)
        Me.button_saveImage.TabIndex = 21
        Me.button_saveImage.Text = "Save"
        Me.button_saveImage.UseVisualStyleBackColor = true
        '
        'textbox_units
        '
        Me.textbox_units.Location = New System.Drawing.Point(586, 47)
        Me.textbox_units.Name = "textbox_units"
        Me.textbox_units.Size = New System.Drawing.Size(81, 20)
        Me.textbox_units.TabIndex = 22
        '
        'button_lineStyle
        '
        Me.button_lineStyle.Location = New System.Drawing.Point(381, 73)
        Me.button_lineStyle.Name = "button_lineStyle"
        Me.button_lineStyle.Size = New System.Drawing.Size(40, 23)
        Me.button_lineStyle.TabIndex = 23
        Me.button_lineStyle.Text = "Line"
        Me.button_lineStyle.UseVisualStyleBackColor = true
        '
        'combobox_penThickness
        '
        Me.combobox_penThickness.FormattingEnabled = true
        Me.combobox_penThickness.Location = New System.Drawing.Point(427, 75)
        Me.combobox_penThickness.Name = "combobox_penThickness"
        Me.combobox_penThickness.Size = New System.Drawing.Size(42, 21)
        Me.combobox_penThickness.TabIndex = 24
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = true
        Me.ComboBox1.Location = New System.Drawing.Point(427, 74)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(42, 21)
        Me.ComboBox1.TabIndex = 24
        '
        'combobox_textSize
        '
        Me.combobox_textSize.FormattingEnabled = true
        Me.combobox_textSize.Location = New System.Drawing.Point(483, 75)
        Me.combobox_textSize.Name = "combobox_textSize"
        Me.combobox_textSize.Size = New System.Drawing.Size(100, 21)
        Me.combobox_textSize.TabIndex = 25
        '
        'ImageControl1
        '
        Me.ImageControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ImageControl1.Image = CType(resources.GetObject("ImageControl1.Image"),System.Drawing.Image)
        Me.ImageControl1.initialimage = Nothing
        Me.ImageControl1.Location = New System.Drawing.Point(12, 103)
        Me.ImageControl1.Mode = ImageControl.DrawingBoard.ModeTypes.Pan
        Me.ImageControl1.Name = "ImageControl1"
        Me.ImageControl1.Origin = New System.Drawing.Point(0, 0)
        Me.ImageControl1.ScrollbarsVisible = true
        Me.ImageControl1.Size = New System.Drawing.Size(752, 77)
        Me.ImageControl1.TabIndex = 0
        Me.ImageControl1.ZoomFactor = 1R
        Me.ImageControl1.ZoomOnMouseWheel = true
        '
        'Form_Measurement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 192)
        Me.Controls.Add(Me.combobox_textSize)
        Me.Controls.Add(Me.combobox_penThickness)
        Me.Controls.Add(Me.button_lineStyle)
        Me.Controls.Add(Me.textbox_units)
        Me.Controls.Add(Me.button_saveImage)
        Me.Controls.Add(Me.button_deleteScale)
        Me.Controls.Add(Me.button_textPosition)
        Me.Controls.Add(Me.button_addScale)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.textbox_pixelsPerUnit)
        Me.Controls.Add(Me.button_newScale)
        Me.Controls.Add(Me.combobox_scales)
        Me.Controls.Add(Me.button_calibrate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.ImageControl1)
        Me.MinimumSize = New System.Drawing.Size(670, 230)
        Me.Name = "Form_Measurement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Microscope measure"
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ImageControl1 As ImageControl.ImageControl
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents radiobutton_panMode As System.Windows.Forms.RadioButton
    Friend WithEvents radiobutton_windowZoom As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents radiobutton_lineDraw As System.Windows.Forms.RadioButton
    Friend WithEvents radiobutton_lineEdit As System.Windows.Forms.RadioButton
    Friend WithEvents button_calibrate As System.Windows.Forms.Button
    Friend WithEvents combobox_scales As System.Windows.Forms.ComboBox
    Friend WithEvents button_newScale As System.Windows.Forms.Button
    Friend WithEvents textbox_pixelsPerUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents button_addScale As System.Windows.Forms.Button
    Friend WithEvents button_textPosition As System.Windows.Forms.Button
    Friend WithEvents button_deleteScale As System.Windows.Forms.Button
    Friend WithEvents button_saveImage As System.Windows.Forms.Button
    Friend WithEvents textbox_units As System.Windows.Forms.TextBox
    Friend WithEvents button_lineStyle As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents combobox_penThickness As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents combobox_textSize As System.Windows.Forms.ComboBox


End Class
