namespace Haushaltsbuch.WebApi.Benutzerkonto.Models
{
    public class SetzeNormalisierteEMailAdresseModel
    {
        public string NormalisierteEMailAdresse { get; set; }

        public SetzeNormalisierteEMailAdresseModel()
        {

        }

        public SetzeNormalisierteEMailAdresseModel(string normalisierteEMailAdresse)
        {
            NormalisierteEMailAdresse = normalisierteEMailAdresse;
        }
    }
}