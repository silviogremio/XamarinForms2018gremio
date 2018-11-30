using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using App1_ConsultarCEP.Servico.Modelo;
using App1_ConsultarCEP.Servico;

namespace Secao05
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BOTAO.Clicked += BuscarCEP;                
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {

                try
                {
                    //ponto mais vulnerável
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if(end.cep != null)
                    {
                        string strJson = "";

                        strJson = string.Format("Endereço:@" +
                                                        "Rua: {0}@" +
                                                        "Bairro: {1}@" +
                                                        "Cidade: {2}@" +
                                                        "Estado: {3}@", end.logradouro, end.bairro, end.localidade, end.uf);

                        strJson = strJson.Replace("@", System.Environment.NewLine);

                        RESULTADO.Text = strJson;
                    }
                    else
                    {
                        DisplayAlert("NÃO EXISTE", "CEP informado é inexistente. CEP informado: " + cep, "OK");
                    }

                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "ok");
                }


            }

        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            //verifica se possui 8 caracteres
            if (cep.Length != 8)
            {
                //título da mensagem, conteúdo e nome do botão
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres","OK");
                valido = false;
            }

            //verifica se possui apenas números
            int novoCEP = 0;
            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números", "OK");
                valido = false;
            }


            return valido;
        }

    }
}
