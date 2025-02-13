Module Program
    Sub Main(args As String())

        GetPlace()

    End Sub

    Sub GetPlace()
        Dim rand As New Random
        Console.WriteLine("Mulai pengambilan data place dari Google Place API...")

        Console.WriteLine("Mendapatkan master Kabpaten/Kota...")
        Dim ar_Kab As String() = Database.GetKab()

        For Each kab As String In ar_Kab
            Console.WriteLine("Mendapatkan master Blok Sensus di Kab/Kota '{0}'...", kab)
            Dim ar_Wilayah As Wilayah() = Database.GetWilayah(kab)

            Console.WriteLine("Mendapatkan data place di Kab/Kota '{0}'...", kab)
            For Each bs As Wilayah In ar_Wilayah
                Console.WriteLine("Mendapatkan data place di Blok Sensus '{0}' dengan koordinat {1},{2}...", bs.idbs, bs.y, bs.x)
                Threading.Thread.Sleep(rand.NextInt64(2, 20) * 1000)
                Dim ob_response As Response = Api.Nearby(bs.y, bs.x).GetAwaiter().GetResult
                Dim b_flag As Boolean = True
                While b_flag AndAlso ob_response.status = "OK" AndAlso ob_response.results.Length > 0
                    Console.WriteLine("Memasukkan data place ke database...")
                    Database.InsertPlace(bs, ob_response.results)

                    If Not String.IsNullOrEmpty(ob_response.next_page_token) Then
                        Threading.Thread.Sleep(2000)
                        ob_response = Api.NearbyNext(ob_response.next_page_token).GetAwaiter().GetResult
                    Else
                        b_flag = False
                    End If
                End While
                Console.WriteLine("Pengambilan data place di Blok Sensus '{0}' selesai...", bs.idbs)
                Database.UpdateStatusWilayah(bs)
            Next
            Console.WriteLine("Pengambilan data place di Kab/Kota '{0}' selesai...", kab)
        Next

        Console.WriteLine("Pengambilan data place dari Google Place API selesai...")
        Console.ReadKey()
    End Sub


End Module
