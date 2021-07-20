Public Class LineDraw

    Public displaySettings As DisplaySettings = Nothing

    Public Sub New(ByVal displaySettings As DisplaySettings)
        Me.displaySettings = displaySettings
    End Sub

    Public Function toScreenSpace(ByVal point As PointF) As PointF
        Dim s As PointF = Nothing
        s.X = ((point.X - displaySettings.origin.X) * displaySettings.zoomFactor)
        s.Y = ((point.Y - displaySettings.origin.Y) * displaySettings.zoomFactor)
        Return s
    End Function

    Public Function toPixelSpace(ByVal point As PointF) As PointF
        Dim st As PointF = Nothing
        st.X = (point.X / displaySettings.zoomFactor) + displaySettings.origin.X
        st.Y = (point.Y / displaySettings.zoomFactor) + displaySettings.origin.Y
        Return st
    End Function

    Public Sub DrawAllMeasurementsAbsolute(ByVal g As Graphics, ByVal lines As List(Of line))
        For Each line As line In lines
            DrawMeasurementAbsolute(g, line)
        Next
    End Sub

    Public Sub drawTextUnitScaleAbsolute(ByVal g As Graphics, ByVal line As line)
        Dim tx As Single = 0
        Dim ty As Single = 0
        tx = (line.end.X - line.start.X) / 2.0 + line.start.X
        ty = (line.end.Y - line.start.Y) / 2.0 + line.start.Y
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        Dim measurement As Single = line.length / displaySettings.pixelsPerUnit
        Dim text As String = measurement.ToString("N2") & displaySettings.units
        Dim rec As RectangleF = measureDisplayStringRectangle(g, text, line.textStyle)

        tx -= (rec.Width / 2.0)
        ty -= (rec.Height * 1.5)

        rec.Location = New Point(tx, ty)
        rec.Width += 1
        rec.Height += 1
        g.FillRectangle(Brushes.White, rec)

        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.DrawString(text, line.textStyle, Brushes.Black, tx, ty)
    End Sub

    Public Sub drawTextAbsolute(ByVal g As Graphics, ByVal line As line)
        Dim tx As Single = 0
        Dim ty As Single = 0

        Select Case line.textPosition
            Case line.position.start
                tx = line.start.X
                ty = line.start.Y
            Case line.position.middle
                tx = (line.end.X - line.start.X) / 2.0 + line.start.X
                ty = (line.end.Y - line.start.Y) / 2.0 + line.start.Y
            Case line.position.end
                tx = line.end.X
                ty = line.end.Y
        End Select

        g.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit

        Dim measurement As Single = line.length / displaySettings.pixelsPerUnit
        Dim text As String = measurement.ToString("N2") & displaySettings.units

        Dim rec As RectangleF = measureDisplayStringRectangle(g, text, line.textStyle)

        tx -= (rec.Width / 2)

        Dim angle As Double = lineAngleDegrees(line.start, line.end)
        angle = ((angle + 270) Mod 360)
        If angle >= 180.0 Then angle -= 180.0
        tx += (Math.Cos(angle / 180.0 * Math.PI) * 20.0)
        ty += (Math.Sin(angle / 180.0 * Math.PI) * 20.0)

        rec.Location = New Point(tx, ty)
        rec.Width += 1
        rec.Height += 1
        g.FillRectangle(Brushes.White, rec)
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.DrawString(text, line.textStyle, Brushes.Black, tx, ty + 1.0)
    End Sub

    Public Function measureDisplayStringRectangle(ByVal graphics As Graphics, ByVal text As String, ByVal font As Font) As RectangleF
        text &= " |"
        Dim format As New System.Drawing.StringFormat()
        Dim rect As New System.Drawing.RectangleF(0, 0, 1000, 1000)
        Dim ranges() As System.Drawing.CharacterRange = {New System.Drawing.CharacterRange(0, text.Length)}
        Dim regions(0) As System.Drawing.Region
        format.SetMeasurableCharacterRanges(ranges)
        regions = graphics.MeasureCharacterRanges(text, font, rect, format)
        Return regions(0).GetBounds(graphics)
    End Function

    Public Function measureDisplayStringWidth(ByVal graphics As Graphics, ByVal text As String, ByVal font As Font) As Integer
        Dim rect As System.Drawing.RectangleF = measureDisplayStringRectangle(graphics, text, font)
        Return CInt(Math.Truncate(rect.Right + 1.0F))
    End Function

    Public Sub DrawMeasurementAbsolute(ByVal g As Graphics, ByVal thisLine As line)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        g.DrawLine(thisLine.pen, thisLine.start, thisLine.end)
        If thisLine.hasIglets Then drawEndLines(g, thisLine)

        If displaySettings.focussedLine Is thisLine Then
            g.DrawLine(Pens.White, thisLine.start, thisLine.end)
        Else
            If displaySettings.hoveringOverLine Is thisLine Then
                g.DrawLine(Pens.Gray, thisLine.start, thisLine.end)
            End If
        End If

        If thisLine.selected Then highlightSelectedEnd(g, thisLine)
        If thisLine.isAUnitScale Then
            drawTextUnitScaleAbsolute(g, thisLine)
        Else
            drawTextAbsolute(g, thisLine)
        End If
    End Sub

    Public Sub highlightSelectedEnd(ByVal g As Graphics, ByVal line As line)
        Dim ar As New RectangleF(If(line.startIsSelected, line.start, line.end), New Size(1, 1))
        ar.Inflate(20, 20)
        g.FillEllipse(New SolidBrush(Color.FromArgb(64, 255, 255, 255)), ar)
    End Sub

    Public Function lineAngleDegrees(ByVal startPoint As PointF, ByVal endPoint As PointF) As Single
        Dim dx As Single = endPoint.X - startPoint.X
        Dim dy As Single = endPoint.Y - startPoint.Y
        Return 180 + (Math.Atan2(dy, dx) * 180.0) / Math.PI
    End Function

    Public Sub drawEndLines(ByVal g As Graphics, ByVal line As line)
        Dim angle As Single = lineAngleDegrees(line.start, line.end)
        angle += 90
        If angle <= 180.0 Then angle += 180.0
        If angle >= 360.0 Then angle -= 180.0
        Dim lineOffsetX As Single = (Math.Cos(angle / 180.0 * Math.PI) * (10 + line.length / 40.0))
        Dim lineOffsetY As Single = (Math.Sin(angle / 180.0 * Math.PI) * (10 + line.length / 40.0))
        Dim startBit As New PointF(line.start.X + lineOffsetX, line.start.Y + lineOffsetY)
        Dim endBit As New PointF(line.end.X + lineOffsetX, line.end.Y + lineOffsetY)
        g.DrawLine(line.pen, line.start, startBit)
        g.DrawLine(line.pen, line.end, endBit)

        Dim rect = New Rectangle(line.start.X - line.pen.Width / 2 + 0.5, line.start.Y - line.pen.Width \ 2, line.pen.Width - 1, line.pen.Width - 1)
        g.FillEllipse(New SolidBrush(line.pen.Color), rect)

        rect = New Rectangle(line.end.X - line.pen.Width / 2 + 0.5, line.end.Y - line.pen.Width \ 2, line.pen.Width - 1, line.pen.Width - 1)
        g.FillEllipse(New SolidBrush(line.pen.Color), rect)

        If displaySettings.focussedLine Is line Then
            g.DrawLine(Pens.White, line.start, startBit)
            g.DrawLine(Pens.White, line.end, endBit)
        Else
            If displaySettings.hoveringOverLine Is line Then
                g.DrawLine(Pens.Gray, line.start, startBit)
                g.DrawLine(Pens.Gray, line.end, endBit)
            End If
        End If
    End Sub


End Class
