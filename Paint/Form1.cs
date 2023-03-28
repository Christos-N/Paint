using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        bool freestyle;
        float x1, y1, fsX1, fsY1;
        int ax, ay, bx, by, count;
        String pick;
       
        private void panel1_MouseMove(object sender, MouseEventArgs e) 
        {
            if (freestyle && radioButton1.Checked)
                g.DrawLine(p, fsX1, fsY1, e.X, e.Y);
            fsX1 = e.X;
            fsY1 = e.Y;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
            freestyle = true;
            fsX1 = e.X;
            fsY1 = e.Y;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked && freestyle)      //Freestyle
                saveToDB1("Freestyle");
            else if (radioButton2.Checked)              //Γραμμή
            {
                g.DrawLine(p, x1, y1, e.X, e.Y);
                saveToDB1("Line");
            }
            else if (radioButton3.Checked)              //Έλλειψη
            {
                g.DrawEllipse(p, x1, y1, fsX1 - x1, fsY1 - y1);
                saveToDB1("Ellipse");
            }
            else if (radioButton4.Checked)              //Ορθογώνιο
            {
                //Εξετάζω τις περιπτώσεις των τιμών ώστε να μπορεί να ζωγραφιστεί ορθογώνιο και με τους 4 τρόπους
                if (fsX1 > x1 && fsY1 > y1)     //Πάνω αριστερά προς κάτω δεξιά
                {
                    g.DrawRectangle(p, x1, y1, fsX1 - x1, fsY1 - y1);
                    saveToDB1("Rectangle");
                }
                else if (fsX1 < x1 && fsY1 < y1)    //Κάτω δεξιά προς πάνω αριστερά
                {
                    g.DrawRectangle(p, fsX1, fsY1, x1 - fsX1, y1 - fsY1);
                    saveToDB1("Rectangle");
                }
                else if (fsX1 > x1 && fsY1 < y1)    //Κάτω αριστερά προς πάνω δεξιά
                {
                    g.DrawRectangle(p, x1, fsY1, fsX1 - x1, y1 - fsY1);
                    saveToDB1("Rectangle");
                }
                else if (fsX1 < x1 && fsY1 > y1)    //Πάνω δεξιά προς κάτω αριστερά
                {
                    g.DrawRectangle(p, fsX1, y1, x1 - fsX1, fsY1 - y1);
                    saveToDB1("Rectangle");
                }
            }
            else if (radioButton5.Checked)      //Κύκλος
            {
                //Εξετάζω τις περιπτώσεις των τιμών ώστε να μπορεί να ζωγραφιστεί κύκλος και με τους 4 τρόπους
                if (fsX1 > x1 && fsY1 > y1)     //Πάνω αριστερά προς κάτω δεξιά
                {
                    g.DrawEllipse(p, x1, y1, (fsX1 - x1 + fsY1 - y1) / 2, (fsX1 - x1 + fsY1 - y1) / 2);
                    saveToDB1("Circle");
                }
                else if (fsX1 < x1 && fsY1 < y1)    //Κάτω δεξιά προς πάνω αριστερά
                {
                    g.DrawEllipse(p, fsX1, fsY1, (x1 - fsX1 + y1 - fsY1) / 2, (x1 - fsX1 + y1 - fsY1) / 2);
                    saveToDB1("Circle");
                }
                else if (fsX1 > x1 && fsY1 < y1)    //Κάτω αριστερά προς πάνω δεξιά
                {
                    g.DrawEllipse(p, x1, fsY1, (fsX1 - x1 + y1 - fsY1) / 2, (fsX1 - x1 + y1 - fsY1) / 2);
                    saveToDB1("Circle");
                }
                else if (fsX1 < x1 && fsY1 > y1)    //Πάνω δεξιά προς κάτω αριστερά
                {
                    g.DrawEllipse(p, fsX1, y1, (x1 - fsX1 + fsY1 - y1) / 2, (x1 - fsX1 + fsY1 - y1) / 2);
                    saveToDB1("Circle");
                }
            }
            freestyle = false;
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Black;
            pictureBox1.BackColor = Color.Black;
        }
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Gray;
            pictureBox1.BackColor = Color.Gray;
        }
        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Brown;
            pictureBox1.BackColor = Color.Brown;
        }
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Red;
            pictureBox1.BackColor = Color.Red;
        }
        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Orange;
            pictureBox1.BackColor = Color.Orange;
        }
        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Yellow;
            pictureBox1.BackColor = Color.Yellow;
        }
        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Green;
            pictureBox1.BackColor = Color.Green;
        }
        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Cyan;
            pictureBox1.BackColor = Color.Cyan;
        }
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Blue;
            pictureBox1.BackColor = Color.Blue;
        }
        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.Color = Color.Purple;
            pictureBox1.BackColor = Color.Purple;
        }

        private void changePenSize(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            p.Width = Convert.ToInt32(item.Text);
        }

        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 250;
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            p = new Pen(Color.Black);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void saveToDB1(string shape)
        {
            string connectionstring = "Data Source=DB1.db;Version=3;";
            SQLiteConnection conn = new SQLiteConnection(connectionstring);
            conn.Open();
            String insertQuery = "INSERT INTO Info (Shape, Timestamp) VALUES (@shape, @time)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, conn);
            command.Parameters.AddWithValue("@shape", shape);
            command.Parameters.AddWithValue("@time", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            command.ExecuteNonQuery();
            conn.Close();
        }

        /*Ενεργοποιώ το χρονόμετρο ώστε να ξεκινήσει να "ζωγραφίζει" και αντίστοιχα σε κάθε button_click αλλάζω την μεταβλητή pick ώστε να καλέσει την κατάλληλη συνάρτηση
         *το timer_tick μέσω του switch. Δίνω και τις κατάλληλες τιμές στις μεταβλητές ax, ay, bx, by για να "ζωγραφίσει" από τις σωστές συντεταγμένες και την τιμή 0 στο count
         *για να ξεκινήσει από την αρχή η συνάρτηση που θα κληθεί. Καλώ την onOrOff με όρισμα false ώστε να κάνει disable όλα τα κουμπιά ώστε να μην μπορεί ο χρήστης να τα "πειράξει".
         */
        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            pick = "House";
            ax = 100;
            ay = 100;
            bx = 200;
            by = 200;
            count = 0;
            onOrOff(false);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            pick = "HI!";
            ax = 300;
            ay = 100;
            bx = 380;
            by = 150;
            count = 0;
            onOrOff(false);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            pick = "Robot";
            ax = 100;
            ay = 300;
            bx = 135;
            by = 335;
            count = 0;
            onOrOff(false);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            pick = "Cube";
            ax = 500;
            ay = 250;
            bx = 550;
            by = 300;
            count = 0;
            onOrOff(false);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (pick)
            {
                case "House":
                    drawHouse();
                    break;
                case "HI!":
                    drawHI();
                    break;
                case "Robot":
                    drawRobot();
                    break;
                case "Cube":
                    drawCube();
                    break;
            }
        }

        /* Σε κάθε συνάρτηση draw χρησιμοποιώ τις μεταβλητές ax, ay, bx, by για τις συντεταγμένες των γραμμών και τη μεταβλητή count για να ζωγραφίζει το πρόγραμμα κομμάτι-κομμάτι
         * και να καταλάβει πότε πρέπει να σταματήσει (else)
         */
        private void drawHouse()
        {
            if (count < 5)  //τετράγωνο
            {
                g.DrawLine(p, ax, ay, ax, ay + 20);
                ay += 20;
                count++;
            }
            else if (count < 10)    //τετράγωνο
            {
                g.DrawLine(p, ax, by, ax + 20, by);
                ax += 20;
                count++;
            }
            else if (count < 15)    //τετράγωνο
            {
                g.DrawLine(p, bx, ay - 20, bx, ay);
                ay -= 20;
                count++;
                by = 100;
            }
            else if (count < 20)    //τετράγωνο
            {
                g.DrawLine(p, ax - 20, by, ax, by);
                ax -= 20;
                count++;
            }
            else if (count < 25)    //τρίγωνο (για την σκεπή)
            {
                g.DrawLine(p, ax, ay, ax + 10, ay - 10);
                ax += 10;
                ay -= 10;
                count++;
            }
            else if (count < 30)    //τρίγωνο
            {
                g.DrawLine(p, ax, ay, ax + 10, ay + 10);
                ax += 10;
                ay += 10;
                count++;
                by = 200;
            }
            else if (count < 35)    //πόρτα
            {
                g.DrawLine(p, 140, by, 140, by - 6);
                by -= 6;
                count++;
                bx = 140;
            }
            else if (count < 40)    //πόρτα
            {
                g.DrawLine(p, bx, 170, bx + 4, 170);
                bx += 4;
                count++;
            }
            else if (count < 45)    //πόρτα
            {
                g.DrawLine(p, bx, by, bx, by + 6);
                by += 6;
                ax = 120;
                ay = 120;
                count++;
            }
            else if (count < 50)    //αριστερό παράθυρο
            {
                g.DrawLine(p, ax, ay, ax, ay + 4);
                ay += 4;
                count++;
            }
            else if (count < 55)    //αριστερό παράθυρο
            {
                g.DrawLine(p, ax, ay, ax + 4, ay);
                ax += 4;
                count++;
            }
            else if (count < 60)    //αριστερό παράθυρο
            {
                g.DrawLine(p, ax, ay - 4, ax, ay);
                ay -= 4;
                count++;
            }
            else if (count < 65)    //αριστερό παράθυρο
            {
                g.DrawLine(p, ax - 4, ay, ax, ay);
                ax -= 4;
                bx = 160;
                by = 120;
                count++;
            }
            else if (count < 70)    //δεξί παράθυρο
            {
                g.DrawLine(p, bx, by, bx, by + 4);
                by += 4;
                count++;
            }
            else if (count < 75)    //δεξί παράθυρο
            {
                g.DrawLine(p, bx, by, bx + 4, by);
                bx += 4;
                count++;
            }
            else if (count < 80)    //δεξί παράθυρο
            {
                g.DrawLine(p, bx, by - 4, bx, by);
                by -= 4;
                count++;
            }
            else if (count < 85)    //δεξί παράθυρο
            {
                g.DrawLine(p, bx - 4, by, bx, by);
                bx -= 4;
                count++;
            }
            else
            {
                timer1.Stop();
                onOrOff(true);
            }
        }
        private void drawHI()
        {
            if (count < 5)  //Η
            {
                g.DrawLine(p, ax, ay, ax, ay + 20);
                ay += 20;
                count++;
            }
            else if (count < 10)  //Η
            {
                g.DrawLine(p, ax, by, ax + 10, by);
                ax += 10;
                count++;
                ay = 100;
            }
            else if (count < 15)  //Η
            {
                g.DrawLine(p, ax, ay, ax, ay + 20);
                ay += 20;
                count++;
                by = 100;
            }
            else if (count < 20)    //Ι
            {
                g.DrawLine(p, bx, by, bx, by + 20);
                by += 20;
                count++;
                ax = 400;
            }
            else if (count < 25)    //!
            {
                g.DrawLine(p, ax, ay, ax, ay - 2);
                ay -= 2;
                count++;
                by = 175;
            }
            else if (count < 30)    //!
            {
                g.DrawLine(p, ax, by, ax, by - 15);
                by -= 15;
                count++;
            }
            else
            {
                timer1.Stop();
                onOrOff(true);
            }
        }
        private void drawRobot()
        {
            if (count < 5)      //μεγάλο τετράγωνο
            {
                g.DrawLine(p, ax, ay, ax, ay + 30);
                ay += 30;
                count++;
            }
            else if (count < 10)      //μεγάλο τετράγωνο
            {
                g.DrawLine(p, ax, ay, ax + 30, ay);
                ax += 30;
                count++;
            }
            else if (count < 15)      //μεγάλο τετράγωνο
            {
                g.DrawLine(p, ax, ay - 30, ax, ay);
                ay -= 30;
                count++;
            }
            else if (count < 20)      //μεγάλο τετράγωνο
            {
                g.DrawLine(p, ax - 30, ay, ax, ay);
                ax -= 30;
                count++;
            }
            else if (count < 25)      //αριστερό μάτι
            {
                g.DrawLine(p, bx, by, bx, by + 5);
                by += 5;
                count++;
            }
            else if (count < 30)      //αριστερό μάτι
            {
                g.DrawLine(p, bx, by, bx + 5, by);
                bx += 5;
                count++;
            }
            else if (count < 35)      //αριστερό μάτι
            {
                g.DrawLine(p, bx, by - 5, bx, by);
                by -= 5;
                count++;
            }
            else if (count < 40)      //αριστερό μάτι
            {
                g.DrawLine(p, bx - 5, by, bx, by);
                bx -= 5;
                count++;
                ax = 190;
                ay = 335;
            }
            else if (count < 45)      //δεξί μάτι
            {
                g.DrawLine(p, ax, ay, ax, ay + 5);
                ay += 5;
                count++;
            }
            else if (count < 50)      //δεξί μάτι
            {
                g.DrawLine(p, ax, ay, ax + 5, ay);
                ax += 5;
                count++;
            }
            else if (count < 55)      //δεξί μάτι
            {
                g.DrawLine(p, ax, ay - 5, ax, ay);
                ay -= 5;
                count++;
            }
            else if (count < 60)      //δεξί μάτι
            {
                g.DrawLine(p, ax - 5, ay, ax, ay);
                ax -= 5;
                count++;
                bx = 175;
                by = 370;
            }
            else if (count < 65)    //μύτη
            {
                g.DrawLine(p, bx, by, bx - 3, by + 6);
                bx -= 3;
                by += 6;
                count++;
            }
            else if (count < 70)    //μύτη
            {
                g.DrawLine(p, bx, by, bx + 5, by);
                bx += 5;
                count++;
                ax = 135;
                ay = 415;
            }
            else if (count < 75)    //στόμα
            {
                g.DrawLine(p, ax, ay, ax, ay + 4);
                ay += 4;
                count++;
            }
            else if (count < 80)    //στόμα
            {
                g.DrawLine(p, ax, ay, ax + 16, ay);
                ax += 16;
                count++;
            }
            else if (count < 85)    //στόμα
            {
                g.DrawLine(p, ax, ay - 4, ax, ay);
                ay -= 4;
                count++;
            }
            else if (count < 90)    //στόμα
            {
                g.DrawLine(p, ax - 16, ay, ax, ay);
                ax -= 16;
                count++;
                bx = 100;
                by = 345;
            }
            else if(count < 95)     //αριστερό αυτί
            {
                g.DrawLine(p, bx, by, bx - 5, by);
                bx -= 5;
                count++;
            }
            else if (count < 100)     //αριστερό αυτί
            {
                g.DrawLine(p, bx, by, bx, by + 10);
                by += 10;
                count++;
            }
            else if (count < 105)     //αριστερό αυτί
            {
                g.DrawLine(p, bx, by, bx + 5, by);
                bx += 5;
                count++;
                ax = 250;
                ay = 345;
            }
            else if (count < 110)     //δεξί αυτί
            {
                g.DrawLine(p, ax, ay, ax + 5, ay);
                ax += 5;
                count++;
            }
            else if (count < 115)     //δεξί αυτί
            {
                g.DrawLine(p, ax, ay, ax, ay + 10);
                ay += 10;
                count++;
            }
            else if (count < 120)     //δεξί αυτί
            {
                g.DrawLine(p, ax, ay, ax - 5, ay);
                ax -= 5;
                count++;
            }
            else
            {
                timer1.Stop();
                onOrOff(true);
            }
        }
        private void drawCube()
        {
            if (count < 5)      //πρώτο τετράγωνο
            {
                g.DrawLine(p, ax, ay, ax, ay + 30);
                ay += 30;
                count++;
            }
            else if (count < 10)      //πρώτο τετράγωνο
            {
                g.DrawLine(p, ax, ay, ax + 30, ay);
                ax += 30;
                count++;
            }
            else if (count < 15)      //πρώτο τετράγωνο
            {
                g.DrawLine(p, ax, ay - 30, ax, ay);
                ay -= 30;
                count++;
            }
            else if (count < 20)      //πρώτο τετράγωνο
            {
                g.DrawLine(p, ax - 30, ay, ax, ay);
                ax -= 30;
                count++;
            }
            else if (count < 25)      //δεύτερο τετράγωνο
            {
                g.DrawLine(p, bx, by, bx, by + 30);
                by += 30;
                count++;
            }
            else if (count < 30)      //δεύτερο τετράγωνο
            {
                g.DrawLine(p, bx, by, bx + 30, by);
                bx += 30;
                count++;
            }
            else if (count < 35)      //δεύτερο τετράγωνο
            {
                g.DrawLine(p, bx, by - 30, bx, by);
                by -= 30;
                count++;
            }
            else if (count < 40)      //δεύτερο τετράγωνο
            {
                g.DrawLine(p, bx - 30, by, bx, by);
                bx -= 30;
                count++;
            }
            else if (count < 45)    //πάνω αριστερά γραμμή
            {
                g.DrawLine(p, ax, ay, ax + 10, ay + 10);
                ax += 10;
                ay += 10;
                count++;
                bx = 650;
                by = 250;
            }
            else if (count < 50)    //πάνω δεξιά γραμμή
            {
                g.DrawLine(p, bx, by, bx + 10, by + 10);
                bx += 10;
                by += 10;
                count++;
                ax = 500;
                ay = 400;
            }
            else if (count < 55)    //κάτω αριστερά γραμμή
            {
                g.DrawLine(p, ax, ay, ax + 10, ay + 10);
                ax += 10;
                ay += 10;
                count++;
                bx = 650;
                by = 400;
            }
            else if (count < 60)    //κάτω δεξιά γραμμή
            {
                g.DrawLine(p, bx, by, bx + 10, by + 10);
                bx += 10;
                by += 10;
                count++;
            }
            else
            {
                timer1.Enabled = false;
                onOrOff(true);
            }
        }

        /*Στην περίπτωση που η συνάρτηση αυτή δεχθεί την τιμή false κάνω disable όλα τα κουμπιά, ώστε να μην μπορεί ο χρήστης να τα "πειράξει".
         *Αν δεχθεί την τιμή true, τα ενεργοποιώ ώστε να τα χρησιμοποιήσει ξανά αν θέλει ο χρήστης
         */
        private void onOrOff(bool state)
        {
            button1.Enabled = state;
            button2.Enabled = state;
            button3.Enabled = state;
            button4.Enabled = state;
        }
    }
}
