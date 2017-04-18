using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maquetteABI
{
    public partial class frmContact : Form
    {
        private Contacts contact;
        public frmContact(ref Contacts contact)
        {
            this.contact = contact;
            InitializeComponent();
        }
       /// <summary>
       /// fermer la fenetre
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnFermerContact_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
       /// <summary>
       /// modifier mon contact
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void btnModifierContact_Click(object sender, EventArgs e)
        {
            // affecter les modif a mon contact
            int a = Int32.Parse(txtNumContact.Text);
            if (this.controle())
            {
                contact.Numero= a;
                contact.Nom= txtNomContact.Text.Trim().ToUpper();
                contact.Prenom= txtPrenomContact.Text.Trim().ToUpper();
                contact.Fonction= txtFonctionContact.Text;
                contact.Mail= txtAdresseMailContact.Text;
                contact.Telephone= txtTelephoneContact.Text;
  
            
                this.DialogResult = DialogResult.OK;

            }
        }
        private void Liste_Contactes_Load(object sender, EventArgs e)
        {
            this.afficheContact(contact);
        }
        /// <summary>
        /// cette procédure affiche en textbox les données d'un contact
        /// </summary>
        /// <param name="unContact"></param>
        private void afficheContact(Contacts contact)
        {
            this.txtNumContact.Text = contact.Numero.ToString();
            this.txtNomContact.Text = contact.Nom.Trim().ToUpper();
            this.txtPrenomContact.Text = contact.Prenom.Trim().ToUpper();
            this.txtFonctionContact.Text = contact.Fonction;
            this.txtAdresseMailContact.Text = contact.Mail;
            this.txtTelephoneContact.Text = contact.Telephone.ToString();
         
        }
        /// <summary>
        /// methode pour gerer les exceptions
        /// </summary>
        /// <returns></returns>
        public Boolean controle()
        {
            Boolean code = true;
            if (!(Outils.EstEntier(this.txtNumContact.Text)))
            {
                code = false;
                MessageBox.Show("Le numero de contact saisi n'est pas un entier valide", "Erreur", MessageBoxButtons.OK);
            }
           
            if (!(Outils.EstEntier(this.txtTelephoneContact.Text)))
            {
                code = false;
                MessageBox.Show("Le numero de telephone  saisi n'est pas correct", "Erreur", MessageBoxButtons.OK);
            }


            if (this.txtNomContact.Text == "")
            {
                code = false;
                MessageBox.Show("veullez indiquer un nom au contact", "Erreur", MessageBoxButtons.OK);
            }

            return code;
        }
    }
}
