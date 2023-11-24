using Microsoft.Data.SqlClient;
using System.Diagnostics;

// 接続文字列の設定

// "Sales.mdf"
string connectionString = @"";

// データベースへの接続準備
using (SqlConnection conn = new SqlConnection())
{

    conn.ConnectionString = connectionString;

    // コマンドの準備
    string query = "SELECT Orders.productId, Products.name, Products.price, Orders.quantity FROM Products, (SELECT Orders.productId, SUM(Orders.quantity) AS quantity FROM Orders, Products WHERE Orders.productId = Products.Id GROUP BY Orders.productId) Orders WHERE Products.Id = Orders.productId ORDER BY Products.Id;";

    using (SqlCommand cmd = new SqlCommand())
    {

        cmd.Connection = conn;
        cmd.CommandText = query;

        // データベース処理は失敗する可能性があるので、例外処理の準備
        try
        {

            // データベースへの接続
            conn.Open();

            // コマンドの実行
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                // 行を読む
                while (reader.Read())
                {

                    // 値を取り出す
                    int productId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int price = reader.GetInt32(2);
                    int quantity = reader.GetInt32(3);

                    // 取り出した値を画面に書く
                    Console.WriteLine($"productId:{productId}, name:{name}, price:{price}, quantity:{quantity}");
                    Console.WriteLine($"売上合計:{price * quantity}");

                }

                // 実行結果の後処理
                reader.Close();

            }

        }
        catch (Exception ex)
        {

            // 例外が出た場合の処理
            Console.WriteLine("エラーが発生しました。[{0}]", ex.Message);

        }
        finally
        {

            // データベースへの接続解除
            conn.Close();

        }

    }

}
