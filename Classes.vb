Public Class Wilayah
    Private s_idbs As String
    Private d_y As Double
    Private d_x As Double
    Public Sub New()
        s_idbs = String.Empty
        d_x = 0.0
        d_y = 0.0
    End Sub
    Public Property idbs As String
        Get
            Return s_idbs
        End Get
        Set(value As String)
            s_idbs = value
        End Set
    End Property
    Public Property x As Double
        Get
            Return d_x
        End Get
        Set(value As Double)
            d_x = value
        End Set
    End Property
    Public Property y As Double
        Get
            Return d_y
        End Get
        Set(value As Double)
            d_y = value
        End Set
    End Property
End Class

Public Class Response
    Private ar_html_attributions As Object()
    Private s_pageToken As String
    Private ar_place As Place()
    Private s_status As String
    Public Sub New()
        ar_html_attributions = Array.Empty(Of Object)
        s_pageToken = String.Empty
        ar_place = Array.Empty(Of Place)
        s_status = String.Empty
    End Sub
    Public Property html_attributions As Object()
        Get
            Return ar_html_attributions
        End Get
        Set(value As Object())
            ar_html_attributions = value
        End Set
    End Property
    Public Property next_page_token As String
        Get
            Return s_pageToken
        End Get
        Set(value As String)
            s_pageToken = value
        End Set
    End Property

    Public Property results As Place()
        Get
            Return ar_place
        End Get
        Set(value As Place())
            ar_place = value
        End Set
    End Property
    Public Property status As String
        Get
            Return s_status
        End Get
        Set(value As String)
            s_status = value
        End Set
    End Property

End Class

Public Class Place
    Private s_businessStatus As String
    Private s_placeId As String
    Private s_name As String
    Private ar_types As String()
    Private s_vicinity As String
    Private ob_geometry As Geometry
    Public Sub New()
        s_businessStatus = String.Empty
        s_placeId = String.Empty
        s_name = String.Empty
        ar_types = Array.Empty(Of String)
        s_vicinity = String.Empty
        ob_geometry = New Geometry()
    End Sub
    Public Property business_status As String
        Get
            Return s_businessStatus
        End Get
        Set(value As String)
            s_businessStatus = value
        End Set
    End Property

    Public Property place_id As String
        Get
            Return s_placeId
        End Get
        Set(value As String)
            s_placeId = value
        End Set
    End Property

    Public Property name As String
        Get
            Return s_name
        End Get
        Set(value As String)
            s_name = value
        End Set
    End Property

    Public Property types As String()
        Get
            Return ar_types
        End Get
        Set(value As String())
            ar_types = value
        End Set
    End Property
    Public Property vicinity As String
        Get
            Return s_vicinity
        End Get
        Set(value As String)
            s_vicinity = value
        End Set
    End Property

    Public Property geometry As Geometry
        Get
            Return ob_geometry
        End Get
        Set(value As Geometry)
            ob_geometry = value
        End Set
    End Property

End Class
Public Class Geometry
    Private ob_location As Location
    Public Sub New()
        ob_location = New Location()
    End Sub
    Public Property location As Location
        Get
            Return ob_location
        End Get
        Set(value As Location)
            ob_location = value
        End Set
    End Property
End Class

Public Class Location
    Private d_y As Double
    Private d_x As Double
    Public Sub New()
        d_y = 0.0
        d_x = 0.0
    End Sub
    Public Property lat As Double
        Get
            Return d_x
        End Get
        Set(value As Double)
            d_x = value
        End Set
    End Property
    Public Property lng As Double
        Get
            Return d_y
        End Get
        Set(value As Double)
            d_y = value
        End Set
    End Property

End Class