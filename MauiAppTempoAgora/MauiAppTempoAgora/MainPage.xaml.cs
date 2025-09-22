using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Descrição do clima: {t.description} \n" +
                                         $"Velocidade do vento: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility} \n" +
                                         $"Nascer do sol: {t.sunrise} \n" +
                                         $"Por do sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n";   

                        lbl_res.Text = dados_previsao;
                    } else
                    {
                        await DisplayAlert("Cidade não encontrada", $"A cidade '{txt_cidade.Text}' não foi localizada.", "OK");
                        lbl_res.Text = "";
                    }
                } else
                {
                    lbl_res.Text = "Preencha a cidade";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão", "Verifique sua conexão com a internet e tente novamente", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
