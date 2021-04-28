using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public class Pedidos
        {
            public int id { get; set; }            
            public string Data { get; set; }
            public DateTime Hora { get; set; }
            public string NomeDoente { get; set; }
            public string Estado { get; set; }

        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string txtout = "";
            //ligaçao ao nosso sql server criado no workbench a password temos de trocar para a nossa, e o database é o mydb3 como eu tinha alterado no script
            string connStr = "server=localhost;user=root;database=mydb;port=3306;password=paulocouto21";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                //Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //query feita á base de dados 
                string sql = "select * from pedidos";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //lê a base de dados enquanto há informação
                while (rdr.Read())
                {
                    txtout += rdr[0] + " -- " + rdr[1] + " -- " + rdr[2] + "--" + rdr[3] + "--" + rdr[4] +";";
                }
                rdr.Close();
            }
            catch (Exception err)
            {
                //Console.WriteLine(err.ToString());
                return new string[] { err.ToString() };
            }
            return new string[] { txtout };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string txtout = "";
            //ligaçao ao nosso sql server criado no workbench a password temos de trocar para a nossa, e o database é o mydb3 como eu tinha alterado no script
            string connStr = "server=localhost;user=root;database=mydb;port=3306;password=paulocouto21";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                //Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //query feita á base de dados 
                string sql = "select * from pedidos";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //lê a base de dados enquanto há informação
                while (rdr.Read())
                {
                    if (Int32.Parse(rdr[0].ToString()) == id)
                        txtout = rdr[0] + " -- " + rdr[1] + " -- " + rdr[2] + "--" + rdr[3] + "--" + rdr[4] + ";";
                }
                rdr.Close();
            }
            catch (Exception err)
            {
                //Console.WriteLine(err.ToString());
                return err.ToString();
            }
            return txtout;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Pedidos pedido)
        {
            string connStr = "server=localhost;user=root;database=mydb;port=3306;password=paulocouto21";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            //query feita á base de dados 
            string sql = "insert into pedidos values (" + pedido.id + "," + pedido.Data + "," + pedido.Hora + ",\"" + pedido.NomeDoente + ",\"" + pedido.Estado + "\"" +");";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Pedidos pedidos)
        {
            string insert = "UPDATE pedidos SET estado = \"cancelado\" WHERE idPedidos = " + (pedidos.id) + ";";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
