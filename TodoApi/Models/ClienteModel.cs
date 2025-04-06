using Microsoft.EntityFrameworkCore;
using TodoApi.Util;

namespace TodoApi.Models;

public class ClienteModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Data_Cadastro { get; set; }
    public string Cpf_Cnpj { get; set; }
    public string Data_Nascimento { get; set; }
    public string Tipo { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string Complemento { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }

    public void RegistrarCliente()
    {
        try
        {
            using (DAL objDAL = new DAL())
            {
                string sql = @"
                INSERT INTO cliente (
                    nome, data_cadastro, cpf_cnpj, data_nascimento, tipo,
                    telefone, email, cep, logradouro, numero, bairro,
                    complemento, cidade, uf
                )
                VALUES (
                    @nome, @data_cadastro, @cpf_cnpj, @data_nascimento, @tipo,
                    @telefone, @email, @cep, @logradouro, @numero, @bairro,
                    @complemento, @cidade, @uf
                )";

                var parametros = new Dictionary<string, object>
            {
                { "@nome", Nome },
                { "@data_cadastro", DateTime.Parse(Data_Cadastro) },
                { "@cpf_cnpj", Cpf_Cnpj },
                { "@data_nascimento", DateTime.Parse(Data_Nascimento) },
                { "@tipo", Tipo },
                { "@telefone", Telefone },
                { "@email", Email },
                { "@cep", Cep },
                { "@logradouro", Logradouro },
                { "@numero", Numero },
                { "@bairro", Bairro },
                { "@complemento", Complemento },
                { "@cidade", Cidade },
                { "@uf", UF }
            };

                objDAL.ExecutarComandoSQL(sql, parametros);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inserir cliente: {ex.Message}");
        }
    }

    public void AtualizarCliente()
    {
        try
        {
            using (DAL objDAL = new DAL())
            {
                string sql = @"
                UPDATE cliente SET
                    nome = @nome,
                    data_cadastro = @data_cadastro,
                    cpf_cnpj = @cpf_cnpj,
                    data_nascimento = @data_nascimento,
                    tipo = @tipo,
                    telefone = @telefone,
                    email = @email,
                    cep = @cep,
                    logradouro = @logradouro,
                    numero = @numero,
                    bairro = @bairro,
                    complemento = @complemento,
                    cidade = @cidade,
                    uf = @uf
                WHERE id = @id;
            ";

                var parametros = new Dictionary<string, object>
            {
                { "@nome", Nome },
                { "@data_cadastro", DateTime.Parse(Data_Cadastro) },
                { "@cpf_cnpj", Cpf_Cnpj },
                { "@data_nascimento", DateTime.Parse(Data_Nascimento) },
                { "@tipo", Tipo },
                { "@telefone", Telefone },
                { "@email", Email },
                { "@cep", Cep },
                { "@logradouro", Logradouro },
                { "@numero", Numero },
                { "@bairro", Bairro },
                { "@complemento", Complemento },
                { "@cidade", Cidade },
                { "@uf", UF },
                { "@id", Id } // importante adicionar o ID também!
            };

                objDAL.ExecutarComandoSQL(sql, parametros);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
        }
    }


    public List<ClienteModel> Listagem()
    {
        List<ClienteModel> lista = new List<ClienteModel>();

        using (DAL objDAL = new DAL())
        {
            string sql = "SELECT * FROM cliente";
            var dataTable = objDAL.RetornarDataTable(sql);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                var cliente = new ClienteModel
                {
                    Id = int.Parse(row["id"].ToString()),
                    Nome = row["nome"].ToString(),
                    Data_Cadastro = DateTime.Parse(row["data_cadastro"].ToString()).ToString("yyyy-MM-dd"),
                    Cpf_Cnpj = row["cpf_cnpj"].ToString(),
                    Data_Nascimento = DateTime.Parse(row["data_nascimento"].ToString()).ToString("yyyy-MM-dd"),
                    Tipo = row["tipo"].ToString(),
                    Telefone = row["telefone"].ToString(),
                    Email = row["email"].ToString(),
                    Cep = row["cep"].ToString(),
                    Logradouro = row["logradouro"].ToString(),
                    Numero = row["numero"].ToString(),
                    Bairro = row["bairro"].ToString(),
                    Complemento = row["complemento"].ToString(),
                    Cidade = row["cidade"].ToString(),
                    UF = row["uf"].ToString()
                };

                lista.Add(cliente);
            }
        }

        return lista;
    }

    public void Excluir(int id)
    {
        using (DAL objDAL = new DAL())
        {
            string sql = "DELETE FROM cliente WHERE id = @id";

            var parametros = new Dictionary<string, object>
        {
            { "@id", id }
        };

            objDAL.ExecutarComandoSQL(sql, parametros);
        }
    }


}