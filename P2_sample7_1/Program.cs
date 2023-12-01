using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using P2_sample7_1;
using System.Collections.Generic;
using System.Diagnostics;

// 接続文字列の設定

// "Sales.mdf"
string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\miki\Source\Repos\P2_enshu7-2\P2_sample7_1\Sales.mdf;Integrated Security=True";

// データベースへの接続準備
using (SqlConnection conn = new SqlConnection())
{

    /*
     * エンティティのコレクションを追加
     */
    Dictionary<int, Product> products = new Dictionary<int, Product>();
    List<Order> orderList = new List<Order>();

    conn.ConnectionString = connectionString;

    // コマンドの準備
    string productsQuery = "SELECT Products.Id, Products.name, Products.price FROM Products ORDER BY Products.Id;";
    string ordersQuery = "SELECT Orders.Id, Orders.productId, Orders.quantity FROM Orders ORDER BY Orders.Id;";

    using (SqlCommand cmd = new SqlCommand())
    {

        cmd.Connection = conn;

        // データベース処理は失敗する可能性があるので、例外処理の準備
        try
        {

            // データベースへの接続
            conn.Open();

            // コマンドの実行

            cmd.CommandText = productsQuery;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                // 行を読む
                while (reader.Read())
                {

                    // 値を取り出す
                    Product product = new ProductObject(
                        // id
                        reader.GetInt32(0),
                        // name
                        reader.GetString(1),
                        // price
                        reader.GetInt32(2));

                    // リストへエンティティを追加する
                    products.Add(product.getId(), product);

                    // 取り出した値を画面に書く
                    Console.WriteLine($"product#getId() -> {product.getId()}\nproduct#getName() -> {product.getName()}\nproduct#getPrice() -> :{product.getPrice()}");

                }

                // 実行結果の後処理
                reader.Close();

            }

            cmd.CommandText = ordersQuery;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                // 発注番号
                int orderNumber = 1;

                // 行を読む
                while (reader.Read())
                {

                    // 値を取り出す
                    OrderItem orderItem = new OrderItem(
                        // id
                        reader.GetInt32(0),
                        // productId
                        reader.GetInt32(1),
                        // quantity
                        reader.GetInt32(2));

                    // 発注情報のリストに商品が存在すれば発注情報を追加する
                    Order order = orderList.getOrder(products[orderItem.getProductId()]);
                    if (orderList.Contains(products[orderItem.getProductId()]))
                    {

                    }
                    orderList.Add(order);

                    // 発注の生成
                    Order order = new Order(orderNumber);

                    // 取り出した値を画面に書く
                    Console.WriteLine($"id:{order.getId()}, productId:{order.getProductId()}, quantity:{order.getQuantity()}");

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

        // 合計金額を計算
        Dictionary<int, int> sum = new Dictionary<int, int>();
        orderList.ForEach(order => {

            int amount = products[order.getProductId()].getPrice() * order.getQuantity();

            if (sum.ContainsKey(order.getProductId()))
            {

                sum[order.getProductId()] += amount;

            }
            else
            {

                sum.Add(order.getProductId(), amount);

            }

        });

        // 合計金額を表示

        foreach (int key in sum.Keys)
        {

            Console.WriteLine($"売上合計: id={key} / sum{sum[key]}");

        }

    }

}
