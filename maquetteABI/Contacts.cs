//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace maquetteABI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contacts
    {
        public int Numero { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Fonction { get; set; }
        public Nullable<int> Numero_Client { get; set; }
        public string Mail { get; set; }
    
        public virtual Clients Clients { get; set; }
    }
}
