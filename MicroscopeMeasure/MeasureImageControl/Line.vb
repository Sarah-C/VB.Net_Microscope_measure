
Public Class line

    Public Enum position
        start
        middle
        [end]
    End Enum

    Private Function pixelLength() As Double
        Dim xLength As Double = Math.Abs(_start.X - _end.X)
        Dim yLength As Double = Math.Abs(_start.Y - _end.Y)
        Dim xySquared As Double = (xLength * xLength) + (yLength * yLength)
        Return Math.Sqrt(xySquared)
    End Function

    Public selected As Boolean = False
    Public startIsSelected As Boolean = True
    Public isAUnitScale As Boolean = False

    Private _start As PointF
    Public Property start() As PointF
        Get
            Return _start
        End Get
        Set(ByVal value As PointF)
            _start = value
            If _end <> PointF.Empty Then _length = pixelLength()
        End Set
    End Property

    Private _end As PointF
    Public Property [end]() As PointF
        Get
            Return _end
        End Get
        Set(ByVal value As PointF)
            _end = value
            If _start <> PointF.Empty Then _length = pixelLength()
        End Set
    End Property

    Private _length As Double = 0
    Public ReadOnly Property length() As Double
        Get
            Return _length
        End Get
    End Property

    Private _pen As Pen = New Pen(Brushes.Blue, 2)
    Public Property pen() As Pen
        Get
            Return _pen
        End Get
        Set(value As Pen)
            _pen = value
        End Set
    End Property

    Private _textPosition As position = position.end
    Public Property textPosition() As position
        Get
            Return _textPosition
        End Get
        Set(value As position)
            _textPosition = value
        End Set
    End Property

    Private _hasIglets As Boolean = True
    Public Property hasIglets() As Boolean
        Get
            Return _hasIglets
        End Get
        Set(value As Boolean)
            _hasIglets = value
        End Set
    End Property

    Private _textStyle As Font = New Font("Arial", 15)
    Public Property textStyle() As Font
        Get
            Return _textStyle
        End Get
        Set(value As Font)
            _textStyle = value
        End Set
    End Property

End Class
