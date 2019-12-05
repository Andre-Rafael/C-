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

namespace TesteHardcore
{
    public class SpedDAO
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
                        cmd.CommandText = "INSERT INTO TABELA0000 (REG,COD_VER,COD_FIN,DT_INI,DT_FIN,NOME,CNPJ,CPF,UF,IE,COD_MUN,IM,SUFRAMA,IND_PERFIL,IND_ATIV,LOCAL_ARQV,NOVO_LOCAL_ARQV) " +
                                                                "VALUES (@REG,@COD_VER,@COD_FIN,@DT_INI,@DT_FIN,@NOME,@CNPJ,@CPF,@UF,@IE,@COD_MUN,@IM,@SUFRAMA,@IND_PERFIL,@IND_ATIV,@LOCAL_ARQV,@NOVO_LOCAL_ARQV)";

                        cmd.Parameters.AddWithValue("@REG", dados[1]);
                        cmd.Parameters.AddWithValue("@COD_VER", dados[2]);
                        cmd.Parameters.AddWithValue("@COD_FIN", dados[3]);
                        cmd.Parameters.AddWithValue("@DT_INI", DateTime.ParseExact(dados[4].ToString(), "ddMMyyyy", CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@DT_FIN", DateTime.ParseExact(dados[5].ToString(), "ddMMyyyy", CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@NOME", dados[6]);
                        cmd.Parameters.AddWithValue("@CNPJ", dados[7]);
                        cmd.Parameters.AddWithValue("@CPF", dados[8]);
                        cmd.Parameters.AddWithValue("@UF", dados[9]);
                        cmd.Parameters.AddWithValue("@IE", dados[10]);
                        cmd.Parameters.AddWithValue("@COD_MUN", dados[11]);
                        cmd.Parameters.AddWithValue("@IM", dados[12]);
                        cmd.Parameters.AddWithValue("@SUFRAMA", dados[13]);
                        cmd.Parameters.AddWithValue("@IND_PERFIL", dados[14]);
                        cmd.Parameters.AddWithValue("@IND_ATIV", dados[15]);
                        cmd.Parameters.AddWithValue("@LOCAL_ARQV", caminho.FileName); 

                        string folderName = @"C:\Users\Andre\Documents\Revio";

                        string pathString = Path.Combine(folderName, dados[7]);

                        //string pathString2 = @"C:\Users\Andre\Documents\Revio\Numero";

                        Directory.CreateDirectory(pathString);

                        string fileName = dados[6] + ".txt";

                        pathString = Path.Combine(pathString, fileName);


                        cmd.Parameters.AddWithValue("@NOVO_LOCAL_ARQV", pathString);
                        dbConnection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro 0000 salvo", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        using (StreamWriter escritor = new StreamWriter(pathString))
                        {
                            escritor.Write("Reg: " + dados[1] +
                                "\nCodigo de verificação: " + dados[2] +
                                "\nCodigo final: " + dados[3] +
                                "\nData de Inicio: " + DateTime.ParseExact(dados[4].ToString(), "ddMMyyyy", CultureInfo.InvariantCulture) +
                                "\nData Final: " + DateTime.ParseExact(dados[5].ToString(), "ddMMyyyy", CultureInfo.InvariantCulture) +
                                "\nNome: " + dados[6] +
                                "\nCNPJ: " + dados[7] +
                                "\nCPF: " + dados[8] +
                                "\nUF: " + dados[9] +
                                "\nUE: " + dados[10] +
                                "\nIE: " + dados[11] +
                                "\nCodigo do Municipio: " + dados[12] +
                                "\nIM: " + dados[13] +
                                "\nSuframa: " + dados[14] +
                                "\nIndentificação Perfil: " + dados[15] +
                                "\nIndetificação Atividade: " + dados[16]);
                        }

                        MessageBox.Show("Esta salvo no " + pathString);
                        break;

                    case "9999":
                        cmd.CommandText = "INSERT INTO TABELA9999 (REG,QTD_LIN) " +
                                                "VALUES (@REG,@QTD_LIN)";

                        cmd.Parameters.AddWithValue("@REG", dados[1]);
                        cmd.Parameters.AddWithValue("@QTD_LIN", dados[2]);

                        dbConnection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro 9999 salvo","Excelente",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
