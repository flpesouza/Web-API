using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TodoApi.Util
{
    public class Autenticacao
    {
        public static string TOKEN = "123";
        public static string FALHA_AUTENTICACAO = "Falha na Autenticação: O token informado é inválido";
        IHttpContextAccessor contextAccessor;

        public Autenticacao(IHttpContextAccessor context)
        {
            contextAccessor = context;
        }

        public void Autenticar()
        {
            try
            {
                string TokenRecebido = contextAccessor.HttpContext.Request.Headers["Token"].ToString();
                if (string.Equals(TOKEN, TokenRecebido) == false)
                {
                    throw new Exception(FALHA_AUTENTICACAO);
                }
            }
            catch
            {
                throw new Exception(FALHA_AUTENTICACAO);
            }
        }
    }
}
