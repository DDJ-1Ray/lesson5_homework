using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace text_8._1
{
    class OrderService
    {
        List<Order> orderlist = new List<Order>();
        public OrderService()
        {
            Order ord1 = new Order(1, "1ray");
            Order ord2 = new Order(2, "2ray");
            ord1.allprice = 100;
            ord2.allprice = 200;
            orderlist.Add(ord1);
            orderlist.Add(ord2);
            //Console.WriteLine(orderlist.Count);
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("請選擇操作");
                Console.WriteLine("+:新增訂單,-:刪除訂單,*:查詢訂單,=:顯示所有訂單,!:排序操作,/:退出操作");
                string str=Console.ReadLine();
                int id = orderlist.Count+1;
                switch (str)
                {
                    case "+":
                        AddNewOrder(id);
                        break;
                    case "-":
                        ReMoveNewOrder();
                        break;
                    case "*":
                        FindOrder();
                        break;
                    case "=":
                        PrinteOrder();
                        break;
                    case "!":
                        ReOrderList();
                        break;
                    case "/":
                        Console.WriteLine("已退出操作");
                        flag = false;
                        break;
                    
                    default:
                        Console.WriteLine("輸入錯誤,請再輸入一次");
                        break;
                }
                //str = null;
                id++;
            }
        }
        public bool AddOrder(Order order)
        {
            try
            {
                foreach(Order ord in orderlist)
                {
                    if (ord.Equals(order))
                    {
                        Console.WriteLine("已有訂單,不能重复增加");
                        return false;
                    }

                }
                orderlist.Add(order);
                Console.WriteLine("增加成功");
                return true;
            }
            catch (System.ArrayTypeMismatchException e)
            {
                Console.WriteLine("增加失敗");
                Console.WriteLine("數組越界");
                return false;
            }
        }
        public bool ReMoveOrder(Order order)
        {
            if (orderlist.Count == 0)
            {
                Console.WriteLine("沒有訂單");
                return false;
            }
            orderlist.Remove(order);
            Console.WriteLine("刪除成功");
            return true;
        }
        public void AddNewOrder(int id)
        {
            Order order;
            OrderItem orderItem;
            
            string storename;
            int price;
            int itemid = 1;
            int count;
            try
            {
                Console.WriteLine("請輸入用戶名:");
                string username = Console.ReadLine();
                Console.WriteLine("請輸入訂單明細數:");
                int orderitemcount = Int32.Parse(Console.ReadLine());
                order = new Order(id, username);
                for (int i = 0; i < orderitemcount; i++)
                {
                    Console.WriteLine("請輸入商品名:");
                    storename = Console.ReadLine();
                    Console.WriteLine("請輸入商品數目:");
                    count = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("請輸入商品單價:");
                    price = Int32.Parse(Console.ReadLine());
                    orderItem = new OrderItem(itemid, storename, count, price);
                    itemid++;
                    order.AddOrderItem(orderItem);
                }
                AddOrder(order);
                //Console.WriteLine(order.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("輸入錯誤,請輸入整數");
            }
        }
        public void ReMoveNewOrder()
        {
            try
            {
                Console.WriteLine("請輸入要刪除的訂單號:");
                int delid = Int32.Parse(Console.ReadLine());
                foreach(Order x in orderlist)
                {
                    if (x.OrderId == delid) {
                        ReMoveOrder(x);
                        break;
                    }
                }
                Console.WriteLine("刪除失敗,沒有訂單");


            }
            catch (FormatException)
            {
                Console.WriteLine("輸入錯誤,請輸入整數");
            }
            
        }
        public void PrinteOrder()
        {
            


            if (orderlist.Count <= 0)
                Console.WriteLine("沒有訂單");
            foreach(Order x in orderlist)
            {
                Console.WriteLine(x.ToString());
            }

        }
        public void FindOrder()
        {
            Console.WriteLine("請選擇查找訂單方式");
            Console.WriteLine("1:訂單號,2:用戶名");
            string str2 = Console.ReadLine();
            try
            {
                switch (str2)
                {
                    case "1":
                        Console.WriteLine("請輸入訂單號:");
                        int id = Int32.Parse(Console.ReadLine());
                        var find1 = from n in orderlist
                                   where n.OrderId == id
                                   //orderby n.allprice
                                   select n;
                        List<Order>list1=find1.ToList();
                        if (list1.Count <= 0)
                        {
                            Console.WriteLine("查找失敗,沒有這個訂單");
                        }
                        foreach(Order x in list1)
                        {
                            Console.WriteLine(x.ToString());
                        }
                        break;

                    case "2":
                        Console.WriteLine("請輸入用戶名:");
                        string username1 = Console.ReadLine();

                        var find2= from n in orderlist
                                   where n.clientname.Equals(username1)
                                   orderby n.allprice
                                   select n;
                        List<Order> list2 = find2.ToList();
                        if (list2.Count <= 0)
                        {
                            Console.WriteLine("查找失敗,沒有這個訂單");
                        }
                        foreach (Order x in list2)
                        {
                            Console.WriteLine(x.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine("輸入錯誤,請輸入數字1或數字2");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("輸入錯誤,請輸入整數");
            }
            

        }
        public bool ReOrderList()
        {
            if (orderlist.Count <= 0)
            {
                Console.WriteLine("沒有訂單");
                return false;
            }
            Console.WriteLine("請選擇排序方式");
            Console.WriteLine("1:訂單號順序,2:訂單號逆序,3:總金額大至小,4:總金額小至大");
            string str3 = Console.ReadLine();
            switch (str3)
            {
                case "1":
                    orderlist.Sort((p1,p2)=>p1.OrderId-p2.OrderId);
                    break;
                case "2":
                    orderlist.Sort((p1, p2) => p2.OrderId - p1.OrderId);
                    break;
                case "3":
                    orderlist.Sort((p1, p2) => p2.allprice - p1.allprice);
                    break;
                case "4":
                    orderlist.Sort((p1, p2) => p1.allprice - p2.allprice);
                    break;
                default:
                    Console.WriteLine("輸入錯誤,請輸入數字1或數字2");
                    break;

            }
            Console.WriteLine("排序成功");
            return true;

        }
    }
}
