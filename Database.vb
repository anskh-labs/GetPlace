Imports System.Data
Imports Microsoft.Data.SqlClient

Module Database
    Private ob_conn As SqlConnection
    Sub New()
        ob_conn = New SqlConnection("Server=.\SQLEXPRESS;Database=prelis;User ID=;Password=;TrustServerCertificate=True;")
    End Sub
    Public Sub InsertPlace(wilayah As Wilayah, results() As Place)
        Try
            If ob_conn.State <> ConnectionState.Open Then ob_conn.Open()
            Dim ob_Cmd = ob_conn.CreateCommand
            ob_Cmd.CommandText = "INSERT INTO [place]([placeId],[placeName],[placeBusinessStatus],[placeAddress],[placeTypes],[placeLatitude],[placeLongitude])VALUES(@placeId,@placeName,@placeBusinessStatus,@placeAddress,@placeTypes,@placeLatitude,@placeLongitude)"
            ob_Cmd.Parameters.Add(New SqlParameter("@placeId", SqlDbType.VarChar))
            ob_Cmd.Parameters.Add(New SqlParameter("@placeName", SqlDbType.VarChar))
            ob_Cmd.Parameters.Add(New SqlParameter("@placeBusinessStatus", SqlDbType.VarChar))
            ob_Cmd.Parameters.Add(New SqlParameter("@placeAddress", SqlDbType.VarChar))
            ob_Cmd.Parameters.Add(New SqlParameter("@placeTypes", SqlDbType.Text))
            ob_Cmd.Parameters.Add(New SqlParameter("@placeLatitude", SqlDbType.Float))
            ob_Cmd.Parameters.Add(New SqlParameter("@placeLongitude", SqlDbType.Float))

            For Each place As Place In results
                ob_Cmd.Parameters("@placeId").Value = place.place_id
                ob_Cmd.Parameters("@placeName").Value = place.name
                ob_Cmd.Parameters("@placeBusinessStatus").Value = place.business_status
                ob_Cmd.Parameters("@placeAddress").Value = place.vicinity
                ob_Cmd.Parameters("@placeTypes").Value = String.Join(",", place.types)
                ob_Cmd.Parameters("@placeLatitude").Value = place.geometry.location.lat
                ob_Cmd.Parameters("@placeLongitude").Value = place.geometry.location.lng
                ob_Cmd.ExecuteNonQuery()
            Next

            ob_conn.Close()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            If ob_conn.State = ConnectionState.Open Then ob_conn.Close()
        End Try
    End Sub

    Public Sub UpdateStatusWilayah(bs As Wilayah)
        Try
            If ob_conn.State <> ConnectionState.Open Then ob_conn.Open()
            Dim ob_Cmd = ob_conn.CreateCommand
            ob_Cmd.CommandText = "UPDATE [bs] SET [status]=1 WHERE [idbs]='" & bs.idbs & "'"
            ob_Cmd.ExecuteNonQuery()
            ob_conn.Close()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            If ob_conn.State = ConnectionState.Open Then ob_conn.Close()
        End Try
    End Sub

    Public Function GetWilayah(Optional kab As String = "") As Wilayah()
        Dim ar_Results As New List(Of Wilayah)
        Try
            If ob_conn.State <> ConnectionState.Open Then ob_conn.Open()
            Dim ob_Cmd = ob_conn.CreateCommand
            kab = IIf(String.IsNullOrEmpty(kab), String.Empty, " AND [kdkab]='" & kab & "'")
            ob_Cmd.CommandText = "SELECT [idbs],[x],[y] FROM [bs] WHERE RIGHT([kdbs],1) IN ('B','K') AND [status]=0" & kab
            Dim ob_Reader = ob_Cmd.ExecuteReader
            While ob_Reader.Read
                Dim ob_Wilayah As New Wilayah With {.idbs = ob_Reader.GetString(0), .x = ob_Reader.GetDouble(1), .y = ob_Reader.GetDouble(2)}
                ar_Results.Add(ob_Wilayah)
            End While
            ob_Reader.Close()
            ob_conn.Close()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            If ob_conn.State = ConnectionState.Open Then ob_conn.Close()
        End Try
        Return ar_Results.ToArray
    End Function

    Public Function GetKab() As String()
        Return {"71", "73", "03", "06", "02", "08", "09", "05", "07", "01", "04", "10"}
    End Function
End Module
