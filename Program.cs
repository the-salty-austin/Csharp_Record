using System;
using System.Data;
using System.Data.SQLite;

namespace SP_Project_v1_0
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = @"URI=file:C:\C#\SummerProject\SP_Project_v1.0\record.sqlite3";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            //cmd.CommandText = "DROP TABLE IF EXISTS Category";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "DROP TABLE IF EXISTS Account";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "DROP TABLE IF EXISTS Entry";
            //cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Category(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, MainCategory TEXT UNIQUE)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Account(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, SubCategory TEXT UNIQUE, category_id INTEGER)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Entry(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, description TEXT, year TEXT,
                                                                month TEXT, day TEXT, ymd INTEGER, price INTEGER, account_id INTEGER)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT OR IGNORE INTO Category(MainCategory) VALUES('Food')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Category(MainCategory) VALUES('Shopping')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Category(MainCategory) VALUES('Entertainment')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Category(MainCategory) VALUES('Transportation')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Category(MainCategory) VALUES('Others')";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Breakfast',1)"; // [group] food
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Lunch',1)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Dinner',1)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Food_Others',1)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Clothing',2)"; // [group] shopping
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Shop_Others',2)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Movie',3)"; // [group] entertainment
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Events',3)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Entertain_Others',3)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Public',4)"; // [group] transportation
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Private',4)"; 
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Trans_Others',4)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT OR IGNORE INTO Account(SubCategory,category_id) VALUES('Others_Others',5)"; // [group] Others
            cmd.ExecuteNonQuery();
            
            // 程式開始執行 

            int price;
            string year;
            string month;
            string day;
            string ymd;
            string description;
            while(true)
            {
                //大類別選擇
                Console.WriteLine("請選擇記帳類別:");
                Console.WriteLine("「飲食」請按a  「購物」請按b  「娛樂」請按c\n「交通」請按d  「其他」請按e 「分析資料」請按f");
                Console.WriteLine("欲結束記帳請直接按Enter");
                string catagory = Console.ReadLine();
                while(true)
                {
                    if (catagory=="a" || catagory=="b" || catagory=="c" || catagory=="d" || catagory=="e" || catagory=="f" || catagory=="")
                    {break;}
                    Console.WriteLine("輸入有誤，請重新選擇:");
                    Console.WriteLine("「飲食」請按a  「購物」請按b  「娛樂」請按c\n「交通」請按d  「其他」請按e 「分析資料」請按f");
                    Console.WriteLine("欲結束記帳程式請直接按Enter");
                    catagory = Console.ReadLine();
                }

                if(catagory=="")
                {break;}

                switch(catagory)
                {
                    case "a": // food
                    while(true)
                    {
                        //細項選擇
                        Console.WriteLine("請選擇記帳細項:");
                        Console.WriteLine("「早餐」請按a  「午餐」請按b  「晚餐」請按c  「其他」請按d");
                        Console.WriteLine("欲返回上一層請直接按Enter");
                        string type = Console.ReadLine();
                        while(true)
                        {
                            if (type=="a" || type=="b" || type=="c" || type=="d" || type=="")
                            {break;}
                            Console.WriteLine("輸入有誤，請重新選擇:");
                            Console.WriteLine("「早餐」請按a  「午餐」請按b  「晚餐」請按c  「其他」請按d");
                            Console.WriteLine("欲返回上一層請直接按Enter");
                            type = Console.ReadLine();
                        }
                        if(type=="")
                        {break;}
                        
                        switch(type)
                        {
                            case "a": // food/breakfast [1]
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,1)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }                            
                            break;

                            case "b": // food/lunch
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,2)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }                 
                            break;

                            case "c": // food/dinner
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,3)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;
                            
                            case "d": // food/others
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,4)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }                
                            break;
                        }
                    }
                    break;
                    
                    case "b": // shopping
                    while(true)
                    {
                        Console.WriteLine("請選擇記帳細項:");
                        Console.WriteLine("「衣物、飾品」請按a  「其他」請按b");
                        Console.WriteLine("欲返回上一層請直接按Enter");
                        string type = Console.ReadLine();
                        while(true)
                        {
                            if (type=="a" || type=="b" || type=="")
                            {break;}
                            Console.WriteLine("輸入有誤，請重新選擇:");
                            Console.WriteLine("「衣物、飾品」請按a  「其他」請按b");
                            Console.WriteLine("欲返回上一層請直接按Enter");
                            type = Console.ReadLine();
                        }
                        if(type=="")
                        {break;}
                        
                        switch(type)
                        {
                            case "a": // shopping/clothing
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,5)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;

                            case "b": // shopping/others
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,6)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;
                        }
                    }
                    break;
                    
                    case "c": // entertainment
                    while(true)
                    {
                        Console.WriteLine("請選擇記帳細項:");
                        Console.WriteLine("「電影」請按a  「活動」請按b  「其他」請按c");
                        Console.WriteLine("欲返回上一層請直接按Enter");
                        string type = Console.ReadLine();
                        while(true)
                        {
                            if (type=="a" || type=="b" || type=="c" || type=="")
                            {break;}
                            Console.WriteLine("輸入有誤，請重新選擇:");
                            Console.WriteLine("「電影」請按a  「活動」請按b  「其他」請按c");
                            Console.WriteLine("欲返回上一層請直接按Enter");
                            type = Console.ReadLine();
                        }
                        if(type=="")
                        {break;}
                        
                        switch(type)
                        {
                            case "a": // entertainment/movie
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,7)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;

                            case "b": // entertainment/events
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,8)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;

                            case "c": // entertainment/others
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,9)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;
                        }
                    }
                    break;
                    
                    case "d": // transportation
                    while(true)
                    {
                        Console.WriteLine("請選擇記帳細項:");
                        Console.WriteLine("「大眾運輸」請按a  「自用車」請按b  「其他」請按c");
                        Console.WriteLine("欲返回上一層請直接按Enter");
                        string type = Console.ReadLine();
                        while(true)
                        {
                            if (type=="a" || type=="b" || type=="c" || type=="")
                            {break;}
                            Console.WriteLine("輸入有誤，請重新選擇:");
                            Console.WriteLine("「大眾運輸」請按a  「自用車」請按b  「其他」請按c");
                            Console.WriteLine("欲返回上一層請直接按Enter");
                            type = Console.ReadLine();
                        }
                        if(type=="")
                        {break;}
                        
                        switch(type)
                        {
                            case "a": // transportation/public
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,10)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }  
                            break;

                            case "b": // transportation/private
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,11)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;

                            case "c": // transportation/others
                            while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,12)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;
                        }
                    }
                    break;
                    
                    case "e": // C_others
                    while (true)
                            {
                                Console.Write("輸入金額: $ ");
                                price=int.Parse(Console.ReadLine());
                                Console.Write("輸入年份 (YYYY): ");
                                year=Console.ReadLine();
                                Console.Write("輸入月份 (MM): ");
                                month=Console.ReadLine();
                                Console.Write("輸入日期 (DD): ");
                                day=Console.ReadLine();
                                Console.Write("內容描述: ");
                                description=Console.ReadLine();
                                ymd = year+month+day;
                                Convert.ToUInt32(ymd);
                                cmd.CommandText = "INSERT INTO Entry(description,year,month,day,ymd,price,account_id) VALUES(@description,@year,@month,@day,@ymd,@price,13)";
                                cmd.Parameters.AddWithValue("@description", description);
                                cmd.Parameters.AddWithValue("@year", year);
                                cmd.Parameters.AddWithValue("@month", month);
                                cmd.Parameters.AddWithValue("@day", day);
                                cmd.Parameters.AddWithValue("@ymd", ymd);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                Console.WriteLine("儲存成功。下列為您輸入的資料：");
                                Console.WriteLine("日期: {0}/{1}/{2} 金額: ${3}\n內容: {4}",year,month,day,price,description);
                                Console.WriteLine("欲繼續輸入下一筆資料請輸入任意文字，欲返回上一層請直接按Enter:");
                                if(Console.ReadLine()=="")
                                {break;}
                            }         
                            break;
                    case "f":  // output data ANALYSIS
                        string stm;
                        while(true)
                        {
                            Console.WriteLine("請選擇資料輸出方式:");
                            Console.WriteLine("「不排列」請按a  「依照花費排列」請按b\n「依照日期先後排列」請按c 「依照分類排列」請按d\n「依照帳目說明排列」請按e 「預算」請按f");
                            Console.WriteLine("欲返回上一層請直接按Enter");
                            string type = Console.ReadLine();
                            while (true)
                            {
                            if (type=="a" || type=="b" || type=="c" || type=="d" || type=="e" || type=="f" || type=="")
                            {break;}
                            Console.WriteLine("輸入有誤，請重新選擇:");
                            Console.WriteLine("「不排列」請按a  「依照花費排列」請按b\n「依照日期先後排列」請按c 「依照分類排列」請按d\n「依照帳目說明排列」請按e 「預算」請按f");
                            Console.WriteLine("欲返回上一層請直接按Enter");
                            type = Console.ReadLine();
                            }
                            if(type=="")
                            {break;}
                        
                            switch(type)
                            {
                            case "a": // random output
                            stm = "SELECT Category.MainCategory,Account.SubCategory,Entry.description,Entry.year,Entry.month,Entry.day,Entry.price,Entry.ymd FROM Category,Entry JOIN Account ON Account.id = Entry.account_id AND Category.id=Account.category_id";
                            {
                                using var cmd_retrieve = new SQLiteCommand(stm, con);
                                using SQLiteDataReader rdr = cmd_retrieve.ExecuteReader();

                                Console.Write($"\n{rdr.GetName(0), -30} {rdr.GetName(1), -16} {rdr.GetName(2), -30} {rdr.GetName(6), 6}");
                                Console.WriteLine("     Date\n");
                                while (rdr.Read())
                                {
                                    Console.Write($@"{rdr.GetString(0), -30} {rdr.GetString(1), -16} {rdr.GetString(2), -30} {rdr.GetInt32(6), 6}");
                                    Console.WriteLine("  {0}/{1}/{2}",rdr.GetString(3),rdr.GetString(4),rdr.GetString(5));
                                }
                                Console.WriteLine("");  
                            }
                            break;

                            case "b": // ordered by expenditure
                            stm = "SELECT Category.MainCategory,Account.SubCategory,Entry.description,Entry.year,Entry.month,Entry.day,Entry.price,Entry.ymd FROM Category,Entry JOIN Account ON Account.id = Entry.account_id AND Category.id=Account.category_id ORDER BY Entry.price DESC";
                            {
                                using var cmd_retrieve = new SQLiteCommand(stm, con);
                                using SQLiteDataReader rdr = cmd_retrieve.ExecuteReader();

                                Console.Write($"\n{rdr.GetName(0), -30} {rdr.GetName(1), -20} {rdr.GetName(2), -30} {rdr.GetName(6), 6}");
                                Console.WriteLine("     Date\n");
                                while (rdr.Read())
                                {
                                    Console.Write($@"{rdr.GetString(0), -30} {rdr.GetString(1), -20} {rdr.GetString(2), -30} {rdr.GetInt32(6), 6}");
                                    Console.WriteLine("  {0}/{1}/{2}",rdr.GetString(3),rdr.GetString(4),rdr.GetString(5));
                                }
                                Console.WriteLine("");   
                            }         
                            break;

                            case "c": // ordered by date
                            stm = "SELECT Category.MainCategory,Account.SubCategory,Entry.description,Entry.year,Entry.month,Entry.day,Entry.price,Entry.ymd FROM Category,Entry JOIN Account ON Account.id = Entry.account_id AND Category.id=Account.category_id ORDER BY Entry.ymd";
                            {
                                using var cmd_retrieve = new SQLiteCommand(stm, con);
                                using SQLiteDataReader rdr = cmd_retrieve.ExecuteReader();

                                Console.Write($"\n{rdr.GetName(0), -30} {rdr.GetName(1), -20} {rdr.GetName(2), -30} {rdr.GetName(6), 6}");
                                Console.WriteLine("     Date\n");
                                while (rdr.Read())
                                {
                                    Console.Write($@"{rdr.GetString(0), -30} {rdr.GetString(1), -20} {rdr.GetString(2), -30} {rdr.GetInt32(6), 6}");
                                    Console.WriteLine("  {0}/{1}/{2}",rdr.GetString(3),rdr.GetString(4),rdr.GetString(5));
                                }
                                Console.WriteLine("");   
                            }         
                            break;

                            case "d": // ordered by Category
                            stm = "SELECT Category.MainCategory,Account.SubCategory,Entry.description,Entry.year,Entry.month,Entry.day,Entry.price,Entry.ymd FROM Category,Entry JOIN Account ON Account.id = Entry.account_id AND Category.id=Account.category_id ORDER BY Category.MainCategory";
                            {
                                using var cmd_retrieve = new SQLiteCommand(stm, con);
                                using SQLiteDataReader rdr = cmd_retrieve.ExecuteReader();

                                Console.Write($"\n{rdr.GetName(0), -30} {rdr.GetName(1), -20} {rdr.GetName(2), -30} {rdr.GetName(6), 6}");
                                Console.WriteLine("     Date\n");
                                while (rdr.Read())
                                {
                                    Console.Write($@"{rdr.GetString(0), -30} {rdr.GetString(1), -20} {rdr.GetString(2), -30} {rdr.GetInt32(6), 6}");
                                    Console.WriteLine("  {0}/{1}/{2}",rdr.GetString(3),rdr.GetString(4),rdr.GetString(5));
                                }
                                Console.WriteLine("");   
                            }         
                            break;

                            case "e": // ordered by description naming
                            stm = "SELECT Category.MainCategory,Account.SubCategory,Entry.description,Entry.year,Entry.month,Entry.day,Entry.price,Entry.ymd FROM Category,Entry JOIN Account ON Account.id = Entry.account_id AND Category.id=Account.category_id ORDER BY Entry.description";
                            {
                                using var cmd_retrieve = new SQLiteCommand(stm, con);
                                using SQLiteDataReader rdr = cmd_retrieve.ExecuteReader();

                                Console.Write($"\n{rdr.GetName(0), -30} {rdr.GetName(1), -20} {rdr.GetName(2), -30} {rdr.GetName(6), 6}");
                                Console.WriteLine("     Date\n");
                                while (rdr.Read())
                                {
                                    Console.Write($@"{rdr.GetString(0), -30} {rdr.GetString(1), -20} {rdr.GetString(2), -30} {rdr.GetInt32(6), 6}");
                                    Console.WriteLine("  {0}/{1}/{2}",rdr.GetString(3),rdr.GetString(4),rdr.GetString(5));
                                }
                                Console.WriteLine("");   
                            }         
                            break;

                            case "f": // BUDGET constraint
                            uint expenditure = 0;
                            float budget = 0;
                            uint search_ymd_begin;
                            uint search_ymd_end;
                            Console.Write("輸入花費額度: $ ");
                            budget = float.Parse(Console.ReadLine());
                            Console.Write("輸入查詢開始日期 (YYYYMMDD) : ");
                            search_ymd_begin = uint.Parse(Console.ReadLine());
                            Console.Write("輸入查詢結束日期 (YYYYMMDD) : ");
                            search_ymd_end = uint.Parse(Console.ReadLine());
                            
                            {
                                stm = @"SELECT price,description,year,month,day,ymd FROM Entry WHERE (ymd<=@end) AND (ymd>=@start) ORDER BY ymd";
                                using var cmd_retrieve = new SQLiteCommand(stm, con);
                                cmd_retrieve.Parameters.Add(new SQLiteParameter("@start", DbType.UInt32));
                                cmd_retrieve.Parameters.Add(new SQLiteParameter("@end", DbType.UInt32));
                                cmd_retrieve.Parameters["@start"].Value = search_ymd_begin;
                                cmd_retrieve.Parameters["@end"].Value = search_ymd_end;
                                cmd_retrieve.ExecuteNonQuery();

                                using SQLiteDataReader rdr = cmd_retrieve.ExecuteReader();

                                Console.Write($"\n{rdr.GetName(1), -30} {rdr.GetName(0), 6}");
                                Console.WriteLine("     Date\n");
                                while (rdr.Read())
                                {
                                    expenditure += (uint)rdr.GetInt32(0);
                                    Console.Write($@"{rdr.GetString(1), -30} {rdr.GetInt32(0), 6}");
                                    Console.WriteLine("  {0}/{1}/{2}",rdr.GetString(2),rdr.GetString(3),rdr.GetString(4));
                                }
                                Console.WriteLine("");  
                            }
                            Console.WriteLine("在 {0} ~ {1} 期間的消費總金額: $ {2}",search_ymd_begin,search_ymd_end,expenditure);
                            if (expenditure>budget) {Console.WriteLine("消費已超過額度");}
                            else if (expenditure>budget*0.9) {Console.WriteLine("注意! 消費已超過額度之90%");}
                            else if (expenditure>budget*0.75) {Console.WriteLine("消費已超過額度之75%");}
                            else if (expenditure>budget*0.5) {Console.WriteLine("消費已超過額度之50%");}
                            else if (expenditure>budget*0.25) {Console.WriteLine("消費已超過額度之25%");}
                                     
                            break;
                            }
                        } // F while loop
                    break; // case f END


                } // END OF ** big_switch **
            } // END OF ** big_while **
        } // END OF ** static void Main(string[] args) **
    }
}       
