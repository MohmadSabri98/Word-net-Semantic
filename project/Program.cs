using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace project
{
    class Program
    {
        private static List<List<Node>> graph = new List<List<Node>>();
        private static Dictionary<int, List<String>> Ids_words = new Dictionary<int, List<string>>();
        private static Dictionary<string, List<int>> Words_id = new Dictionary<string, List<int>>();
        //private static List<string> Words;
        //private static List<int> IDS;
        private static int num_of_nodes = 100000;
        private static int oo = 1000000;
        private static string[] split = new string[10000];
        private static int f_ans = 0;
        private static Node lca;
        public static int lca_dist(Node A, Node B, List<List<Node>> graph)
        {
            int[] colors = Enumerable.Repeat(-1, num_of_nodes + 1).ToArray();
            int[] dist = Enumerable.Repeat(oo, num_of_nodes).ToArray();// intialize the array with oo 
            int[] dist1 = Enumerable.Repeat(oo, num_of_nodes + 1).ToArray();
            if (A != null && B != null)
            {
                Queue<Node> q = new Queue<Node>();
                A.node_color = 1;
                q.Enqueue(A);
                colors[A.node_value] = 1;
                dist[A.node_value] = 0;
            
                while (q.Count > 0)
                {
                    Node current = q.Dequeue();

                    if (current == null)
                        continue;
                    int i = 0;

                    foreach (Node cc in graph[current.node_value])
                    {
               
                        if (dist[cc.node_value] == oo)
                        {
                            if (cc.node_value == B.node_value)
                                colors[B.node_value] = 1;
                            dist[cc.node_value] = dist[current.node_value] + 1;
                            graph[current.node_value][i].node_color = 1;
                           
                            colors[graph[current.node_value][i].node_value] = 1;
                            //                         Console.WriteLine(colors[5]);

                            q.Enqueue(cc);
                        }
                        i++;

                    }
                }// begin red
            }
         
            int yi=0;

            int ans = oo;
            dist1[B.node_value] = 0;

           
            if (colors[B.node_value] == 1)
            {
            
                B.node_color = 0;
                colors[B.node_value] = 0;
                lca = B;
                ans = dist1[B.node_value] + dist[B.node_value];


            }
            Queue<Node> q2 = new Queue<Node>();
            q2.Enqueue(B);
            B.node_color = 0;
            while (q2.Count > 0)
            {
                Node current1 = q2.Dequeue();
                if (current1 == null)
                    continue;
                int j = 0;
                foreach (Node cc in graph[current1.node_value])
                {
                    if (dist1[cc.node_value] == oo)
                    {
                        dist1[cc.node_value] = dist1[current1.node_value] + 1;

                   
                        if (colors[cc.node_value] == 1)
                        {
                            graph[current1.node_value][j].node_color = 0;// make red
                             yi=dist[graph[current1.node_value][j].node_value] + dist1[graph[current1.node_value][j].node_value];
                           
                            if (yi <= ans)
                            {
    
                                lca = graph[current1.node_value][j];
                                ans = dist[graph[current1.node_value][j].node_value] + dist1[graph[current1.node_value][j].node_value];
                        
                            }


                        }
                        else
                        {
                            graph[current1.node_value][j].node_color = 0;// make red
                            colors[graph[current1.node_value][j].node_value] = 0;
                            // Console.WriteLine("dddddddddddddddd");
                        }

                    }
                    q2.Enqueue(cc);
                    j++;
                }
            }


            f_ans = ans;
            return f_ans;

        }//lca;
        private static string removewhite(string input)
        {
            int j = 0, inputlen = input.Length;
            char[] newarr = new char[inputlen];
            for (int i = 0; i < inputlen; i++)
            {
                char tmp = input[i];
                if (!char.IsWhiteSpace(tmp))
                {
                    newarr[j] = tmp;
                    ++j;

                }
            }
            return new string(newarr, 0, j);
        }
        private static void Read_graphFile(string path)
        {

            if (!File.Exists(path))
                return;
            string[] lines;
            lines = System.IO.File.ReadAllLines(path);
            int w = lines.Length;
            Console.WriteLine(w);
            //num_of_nodes = w + 1;
            char[] del = new char[] { ',' };
            string[] nodes;
            int z = 0, d = 0;
            Node asi = new Node();
            List<Node> li = new List<Node>();
            for (int i = 0; i < w+1; i++)
            {
                graph.Add(new List<Node>());
            }

            for (int i = 0; i < w; i++)
            {

                nodes = lines[i].Split(del, StringSplitOptions.RemoveEmptyEntries);

                z = int.Parse(nodes[0]);
                for (int j = 1; j < nodes.Length; j++)
                {

                    d = int.Parse(nodes[j]);
                    asi = new Node();
                    asi.node_value = d;
                    graph[z].Add(asi);

                }



            }
        }
        private static void Read_mapFile(string path)
        {
            if (!File.Exists(path))
                return;
            string[] lines;
            lines = System.IO.File.ReadAllLines(path);
            int w = lines.Length;
          
            Console.WriteLine(w);
            char[] del = new char[] { ',' };
            char[] del2 = new char[] { ' ' };
            string[] words,realwords;
            int Id = 0;
       
            string rr = "";
            List<string> li ;
            List<int> li2;
            for (int i = 0; i < w; i++)
            {
        
                li = new List<string>();
                li2 = new List<int>();
                    words = lines[i].Split(del, StringSplitOptions.RemoveEmptyEntries);
                    Id = int.Parse(words[0]);
                    realwords = words[1].Split(del2, StringSplitOptions.RemoveEmptyEntries);
                    li2.Add(Id);    
                
                for (int j = 0; j < realwords.Length; j++)
                    {
                    
                    
                        rr = realwords[j];

                        if (Words_id.ContainsKey(rr))
                        {
                            List<int> list = Words_id[rr];
                            if (list.Contains(Id) == false)
                            {
                                list.Add(Id);
                            }
                        }

                        else
                        {
                            List<int> list = new List<int>();
                            list.Add(Id);
                            Words_id.Add(rr, list);



                        }
                       
                        
                      li.Add(realwords[j]); ;
                    }
                Ids_words.Add(Id, li);
             
            }


        }
        private static List<string> Get_words(int Id)
        {
            if (Ids_words.ContainsKey(Id))
            {
                 return Ids_words[Id];
                
            }
            return null;

        }// two mapping functions
        private static List<int> Get_id(string word)
        {
            if (Words_id.ContainsKey(word))
            {
                return  Words_id[word];
            }
            return null;
            
        }
       private static void readinput_file(string path)
        {
            if (!File.Exists(path))
                return;
            FileInfo fi = new FileInfo("large1\\output1.txt");
            StreamWriter st = new StreamWriter("large1\\output1.txt", true);
       
           string[] lines, input_ids;
            lines = System.IO.File.ReadAllLines(path);
            int test_cases = int.Parse(lines[0]);
            char[] del = new char[] { ',' };
            Node A;
            Node B;
            List<string> answ;
           int you=0;
            List<int> id1, id2;
            int sum = 0;
             int min = oo;
            for (int i = 1; i < lines.Length; i++)
            {
                sum = 0;
                min = oo;
                input_ids = lines[i].Split(del, StringSplitOptions.RemoveEmptyEntries);
                //Console.WriteLine("hhhh");
              id1=   Get_id(removewhite(  input_ids[0]));
              //Console.WriteLine("hhhh");
              id2 = Get_id(removewhite(input_ids[1]));
              if (id2 != null & id2 != null)
              {
                  for (int j = 0; j < id1.Count; j++)
                  {
                      for (int k = 0; k < id2.Count; k++)
                      {
                          A = new Node();
                          A.node_value = id1[j];
                          B = new Node();
                          B.node_value = id2[k];
                          sum = lca_dist(A, B, graph);
                        /* if (i == 3)
                              Console.WriteLine(sum + "       " + "line 3"+A.node_value+"  "+B.node_value+" "+input_ids[0]+" "+input_ids[1]);
                         */ 
                         if (sum <= min)
                          {
                              min = sum;
                           you   = lca.node_value;
                          }
                      }
               
                      
                     
                  }

               
                 
                  answ = Get_words(you);
                 
                  Console.WriteLine(min + "," + answ[0]);
                  st.WriteLine((min + "," + answ[0]));
              }

             

            }
            st.Close();
        }
      private static void Readinput2(string path)
      {
     
          int maxl = oo,maxg=-oo;
          if (!File.Exists(path))
              return;
          string[] lines;
          lines = System.IO.File.ReadAllLines(path);
          int test_cases = int.Parse(lines[0]);
          char[] del = new char[] { ',' };
          string[] arr;
          Node A, B;
          int sum = 0;
          int ans = 0;
          string outcast = "";
          List<int> i1,i2;
          //string anss = "";
for (int i = 1; i < lines.Length; i++)
{
arr = lines[i].Split(del, StringSplitOptions.RemoveEmptyEntries);
ans = 0; maxl = oo; maxg = -oo;sum=0;
for (int j = 0; j < arr.Length; j++)
{
    maxl = oo; sum = 0; ans = 0;
if (j + 1 == arr.Length)
{
arr[j] = removewhite(arr[j]);
i1 = Get_id(arr[j]);
}
else
i1 = Get_id(arr[j]);
for (int k = 0; k < arr.Length; k++)
{
if (k != j)
{
maxl = oo;
if (k + 1 == arr.Length)
{
arr[k] = removewhite(arr[k]);
i2 = Get_id(arr[k]);
}
else
i2 = Get_id(arr[k]);
if (i2 != null && i1 != null)
{
    sum = 0;
for (int p = 0; p < i1.Count; p++)
{
A = new Node();
A.node_value = i1[p];
for (int d = 0; d < i2.Count; d++)
{
    B = new Node();
    B.node_value = i2[d];
    sum = lca_dist(A, B, graph);
    if (sum <= maxl)
    { maxl = sum; }
}

}

}
ans += maxl;
}
else
ans += 0;
}
if (ans > maxg)
{
maxg = ans;
outcast = arr[j];
//Console.WriteLine(outcast+"kkk");
}

}
Console.WriteLine(outcast);

}//great
                        

      }// func
   
        static void Main(string[] args)
        {
            string graph_filepath = "large3\\gl.txt";
            string map_filepath = "large3\\ml.txt";
            string input1 = "large3\\in1.txt";
            Read_graphFile(graph_filepath);
          Read_mapFile(map_filepath);
         readinput_file(input1);
            Node ee1 = new Node(); ee1.node_value = 34118;
            Node ee2 = new Node(); ee2.node_value = 9750;

        }


    }
}
