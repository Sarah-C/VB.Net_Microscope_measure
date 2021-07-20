Public Class PointToLine

    Public Shared Function sqr(ByVal x As Double) As Double
        Return x * x
    End Function

    Public Shared Function dist2(ByVal v As PointF, ByVal w As PointF) As Double
        Return sqr(v.X - w.X) + sqr(v.Y - w.Y)
    End Function

    Public Shared Function distToSegmentSquared(ByVal p As PointF, ByVal v As PointF, ByVal w As PointF) As Double
        Dim l2 As Double = dist2(v, w)
        If l2 = 0 Then Return dist2(p, v)
        Dim t As Double = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / l2
        If t < 0 Then Return dist2(p, v)
        If t > 1 Then Return dist2(p, w)
        Return dist2(p, New Point(v.X + t * (w.X - v.X), v.Y + t * (w.Y - v.Y)))
    End Function

    Public Shared Function distToSegment(ByVal point As PointF, ByVal startPoint As PointF, ByVal endPoint As PointF) As Double
        Return Math.Sqrt(distToSegmentSquared(point, startPoint, endPoint))
    End Function

End Class
