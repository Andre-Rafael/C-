using Sped.Lib.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteHardcore.DAO
{
    public class SpedContribDAO
    {
        public void Inserir(string[] dados, OpenFileDialog caminho)
        {
            MdbStringConnection strCon = MdbStringConnection.GetInstance;
            string connec = strCon.MSSQLServerEFCore(".", "teste", "sa", "Zaq123@");
            using (var dbConnection = new SqlConnection(connec))
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = dbConnection;

                switch (dados[1])
                {
                    case "0000":
                        cmd.CommandText = "insert into Cont_0000 (REG,COD_VER,TIPO_ESCRIT,IND_SIT_ESP,NUM_REC_ANTERIOR,DT_INI,DT_FIM,NOME,CNPJ,UF,COD_MUN,SUFRAMA,IND_NAT_PJ,IND_ATIV,LOCAL_ORIGEM,LOCAL_DESTINO)" +
                            "values (@REG,@COD_VER,@TIPO_ESCRIT,@IND_SIT_ESP,@NUM_REC_ANTERIOR,@DT_INI,@DT_FIM,@NOME,@CNPJ,@UF,@COD_MUN,@SUFRAMA,@IND_NAT_PJ,@IND_ATIV,@LOCAL_ORIGEM,@LOCAL_DESTINO)";

                        cmd.Parameters.AddWithValue("@REG", dados[1]);
                        cmd.Parameters.AddWithValue("@COD_VER", dados[2]);
                        cmd.Parameters.AddWithValue("@TIPO_ESCRIT", dados[3]);
                        cmd.Parameters.AddWithValue("@IND_SIT_ESP", dados[4]);
                        cmd.Parameters.AddWithValue("@NUM_REC_ANTERIOR", dados[5]);
                        cmd.Parameters.AddWithValue("@DT_INI", DateTime.ParseExact(dados[6].ToString(), "ddMMyyyy", CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@DT_FIM", DateTime.ParseExact(dados[7].ToString(), "ddMMyyyy", CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@NOME", dados[8]);
                        cmd.Parameters.AddWithValue("@CNPJ", dados[9]);
                        cmd.Parameters.AddWithValue("@UF", dados[10]);
                        cmd.Parameters.AddWithValue("@COD_MUN", dados[11]);
                        cmd.Parameters.AddWithValue("@SUFRAMA", dados[12]);
                        cmd.Parameters.AddWithValue("@IND_NAT_PJ", dados[13]);
                        cmd.Parameters.AddWithValue("@IND_ATIV", dados[14]);
                        cmd.Parameters.AddWithValue("@LOCAL_ORIGEM", caminho.FileName);

                        string folderName = @"C:\Users\Andre\Documents\Revio";

                        string pathString = Path.Combine(folderName, dados[9]);

                        Directory.CreateDirectory(pathString);

                        string fileName = dados[9] + ".txt";

                        pathString = Path.Combine(pathString, fileName);

                        cmd.Parameters.AddWithValue("@LOCAL_DESTINO", pathString);
                        dbConnection.Open();
                        cmd.ExecuteNonQuery()
;
                        MessageBox.Show("Very Nice");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
