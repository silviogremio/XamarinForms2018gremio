using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using App1_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App1_ConsultarCEP.Servico
{
    public class ViaCEPServico
    {
		//utilizada para fazer download/obter endereço preenchido
		private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

		public static Endereco BuscarEnderecoViaCEP(string cep)
		{
			string NovoEnderecoURL = string.Format(EnderecoURL, cep);

            //método síncrono trava a tela e não faz nada em cima do plano
            WebClient wc = new WebClient();            
            string conteudo = wc.DownloadString(NovoEnderecoURL);

            //desserializar é pegar o JSON e converter para um objeto do tipo endereço
            //NewtonSoftware é uma biblioteca popular para desserializar
            //Desserializando o JSon que está na variável conteudo, definido como tipo saída Endereco
            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo);

            //JSON retorna null quando o CEP informado não existe.
            if (end.cep == null) return null;

            return end;
            
		}
    }
}
