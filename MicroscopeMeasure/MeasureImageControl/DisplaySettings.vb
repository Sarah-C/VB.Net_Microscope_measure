Public Class DisplaySettings


    Private _font As Font = New Font("Arial", 12)
    Public Property font() As Font
        Get
            Return _font
        End Get
        Set(ByVal value As Font)
            _font = value
        End Set
    End Property

    Private _linePen As Pen = New Pen(Brushes.Blue, 2)
    Public Property linePen() As Pen
        Get
            Return _linePen
        End Get
        Set(ByVal value As Pen)
            _linePen = value
        End Set
    End Property

    Private _hoveringOverLine As line = Nothing
    Public Property hoveringOverLine() As line
        Get
            Return _hoveringOverLine
        End Get
        Set(ByVal value As line)
            _hoveringOverLine = value
        End Set
    End Property

    Private _focussedLine As line = Nothing
    Public Property focussedLine() As line
        Get
            Return _focussedLine
        End Get
        Set(ByVal value As line)
            _focussedLine = value
        End Set
    End Property

    Private _origin As Point
    Public Property origin() As Point
        Get
            Return _origin
        End Get
        Set(ByVal value As Point)
            _origin = value
        End Set
    End Property

    Private _zoomFactor As Double
    Public Property zoomFactor() As Double
        Get
            Return _zoomFactor
        End Get
        Set(ByVal value As Double)
            _zoomFactor = value
        End Set
    End Property

    Private _units As String = " mm"
    Public Property units() As String
        Get
            Return _units
        End Get
        Set(ByVal value As String)
            _units = value
        End Set
    End Property

    Private _pixelsPerUnit As Double = 100
    Public Property pixelsPerUnit() As Double
        Get
            Return _pixelsPerUnit
        End Get
        Set(ByVal value As Double)
            _pixelsPerUnit = value
        End Set
    End Property

End Class
