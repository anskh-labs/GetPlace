Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json

Module Api
    Private ob_client As New HttpClient
    Private s_key As String
    Sub New()
        ob_client = New HttpClient()
        ob_client.BaseAddress = New Uri("https://maps.googleapis.com/maps/api/place/")
        s_key = ""
    End Sub
    Public Async Function Nearby(y As Double, x As Double) As Task(Of Response)
        Dim ob_response As New Response

        Dim s_url As String = String.Format("nearbysearch/json?key={0}&location={1},{2}&rankby=distance", s_key, y, x)
        Try
            Dim ob_responseMessage = Await ob_client.GetAsync(s_url)
            If ob_responseMessage.IsSuccessStatusCode Then
                Dim s_content = Await ob_responseMessage.Content.ReadAsStringAsync()
                ob_response = CType(JsonConvert.DeserializeObject(s_content, GetType(Response)), Response)
            Else
                Console.WriteLine(ob_responseMessage.ReasonPhrase)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return ob_response
    End Function

    Public Async Function NearbyNext(token As String) As Task(Of Response)
        Dim ob_response As New Response

        Dim s_url As String = String.Format("nearbysearch/json?key={0}&pagetoken={1}", s_key, token)
        Try
            Dim ob_responseMessage = Await ob_client.GetAsync(s_url)
            If ob_responseMessage.IsSuccessStatusCode Then
                Dim s_content = Await ob_responseMessage.Content.ReadAsStringAsync()
                ob_response = CType(JsonConvert.DeserializeObject(s_content, GetType(Response)), Response)
            Else
                Console.WriteLine(ob_responseMessage.ReasonPhrase)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return ob_response
    End Function
End Module
