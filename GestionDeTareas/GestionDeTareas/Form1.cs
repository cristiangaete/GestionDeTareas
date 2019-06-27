using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeTareas
{
    public partial class Form1 : Form
    {
        private bool _IsNewTask;
        private bool _HasChanges;

        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            this.lstTask.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.txtTast.Enabled = false;
            this.btnAdd.Enabled = true;
            this.txtTast.Text = "";
            //Activamos o descativamos el listbox dependiendo de la cantidad
            this.lstTask.Enabled = this.lstTask.Items.Count > 0;
            this.lstTask.SelectedIndex = -1;

            _HasChanges = false;

        }

        private void AddNewTask()
        {
            if (_HasChanges)
            {
                if (MessageBox.Show("¿Guardar cambios?", "Guardar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   if (!this.SaveChanges())
                    {
                        return;
                    }
                }
            }
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnAdd.Enabled = false;
            this.txtTast.Enabled = true;
            this.txtTast.Text = "";
            this.txtTast.Focus();
            _IsNewTask = true;
            
        }

        private void DeleteTask()
        {
            if (MessageBox.Show("¿Esta seguro que desea elminar?", "Confirmar eliminacion", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                if (lstTask.SelectedIndex >= 0 && lstTask.SelectedIndex < lstTask.Items.Count)
                {
                    this.lstTask.Items.RemoveAt(this.lstTask.SelectedIndex);
                    this.Reset();
                }
            }
            
        }

        private bool SaveChanges()
        {
            if (txtTast.Text.Length == 0)
            {
                MessageBox.Show("Debe escribir un nombre para la tarea", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_IsNewTask)
            {
                this.lstTask.Items.Add(this.txtTast.Text);
                this.Reset();
            }
            else
            {
                //Aqui edita el valor del item de lstTask
                this.lstTask.Items[this.lstTask.SelectedIndex] = this.txtTast.Text;
                MessageBox.Show("Guardado correctamente");
            }

            return true;
            
            
        }

        private void Cancel()
        {
            if (_HasChanges)
            {
                if (MessageBox.Show("¿Guardar cambios?", "Guardar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!this.SaveChanges())
                    {
                        return;
                    }
                }
            }

            this.Reset();
        }

       private void LoadSelectedTask()
        {
            if (lstTask.SelectedIndex >= 0 && lstTask.SelectedIndex <  lstTask.Items.Count)
            {
                //Recupera la posiciopn de la tarea que seleccionamos
                this.txtTast.Text = lstTask.Items[lstTask.SelectedIndex].ToString();

                this.btnSave.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnCancel.Enabled = true;
                
                this.txtTast.Enabled = true;

                _IsNewTask = false;
            }
            
        }




        private void BtnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteTask();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddNewTask();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Cancel();
        }

        private void lstTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSelectedTask();
        }

        private void txtTast_TextChanged(object sender, EventArgs e)
        {
            _HasChanges = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           DialogResult result =  MessageBox.Show("¿Guardar cambios?", "Guardar", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                if (!this.SaveChanges())
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
               
            
        }
    }
}
