Public Class Measurements

    Public lines As New List(Of line)
    Public displaySettings As DisplaySettings = Nothing
    Public lineDraw As LineDraw = Nothing

    Public Sub New(ByVal displaySettings As DisplaySettings)
        Me.displaySettings = displaySettings
        lineDraw = New LineDraw(displaySettings)
    End Sub

    Private Function distance(ByVal point1 As PointF, ByVal point2 As PointF) As Double
        Dim xLength As Double = Math.Abs(point1.X - point2.X)
        Dim yLength As Double = Math.Abs(point1.Y - point2.Y)
        Dim xySquared As Double = (xLength * xLength) + (yLength * yLength)
        Return Math.Sqrt(xySquared)
    End Function

    Public Function selectClosestLine(ByVal mousePos As PointF) As line
        Dim selectedLine As line = Nothing
        Dim closestDistance As Double = 100000000
        For Each line As line In lines
            Dim distance As Single = PointToLine.distToSegment(mousePos, line.start, line.end)
            If distance < closestDistance Then
                closestDistance = distance
                selectedLine = line
            End If
        Next
        If closestDistance < 20.0 Then
            Return selectedLine
        End If
        Return Nothing
    End Function

    Public Function selectClosestLineByEnd(ByVal mousePos As PointF) As line
        Dim selectedLine As line = Nothing
        Dim startSelected As Boolean = False
        Dim closestDistance As Double = 100000000
        For Each line As line In lines
            If line.isAUnitScale Then Continue For
            line.selected = False
            Dim distanceToStart As Single = distance(line.start, mousePos)
            If distanceToStart < closestDistance Then
                closestDistance = distanceToStart
                selectedLine = line
                startSelected = True
            End If

            Dim distanceToEnd As Single = distance(line.end, mousePos)
            If distanceToEnd < closestDistance Then
                closestDistance = distanceToEnd
                selectedLine = line
                startSelected = False
            End If
        Next
        If closestDistance < 20.0 Then
            selectedLine.startIsSelected = startSelected
            selectedLine.selected = True
            Return selectedLine
        End If
        Return Nothing
    End Function

    Public Function addLineScreenSpace(ByVal startPoint As PointF, ByVal endPoint As PointF) As line
        Dim l As New line()
        l.pen = displaySettings.linePen
        l.textStyle = displaySettings.font
        updateLineScreenSpace(l, startPoint, endPoint)
        lines.Add(l)
        Return l
    End Function

    Public Function addLineImageSpace(ByVal startPoint As PointF, ByVal endPoint As PointF) As line
        Dim l As New line()
        l.pen = displaySettings.linePen
        l.textStyle = displaySettings.font
        l.start = startPoint
        l.end = endPoint
        lines.Add(l)
        Return l
    End Function

    Public Sub updateLineScreenSpace(ByVal line As line, ByVal startPoint As PointF, ByVal endPoint As PointF)
        line.start = lineDraw.toPixelSpace(startPoint)
        line.end = lineDraw.toPixelSpace(endPoint)
    End Sub

    Public Sub DrawAllMeasurementsAbsolute(ByVal g As Graphics)
        lineDraw.DrawAllMeasurementsAbsolute(g, lines)
    End Sub

    Public Sub DrawMeasurementAbsolute(ByVal g As Graphics, ByVal line As line)
        lineDraw.DrawMeasurementAbsolute(g, line)
    End Sub

End Class
