using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Пространство_функций
{
	public partial class Form1 : Form
    {
		double CRe;
		double CIm;
		int Cmode;
		double m;
		int Lx, Ly, dx, dy;
		double k, sk, sx, sy, ssx, ssy;
        int x; int y;
        bool dC;
		int sn;
		double t, dt;
		double dist;
		bool animation = false;
		Graphics gr;
		Bitmap bitmap;
		Pen pen;
		Complex c;

		public Form1()
        {
			InitializeComponent();
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
			CRe = -1;
			CIm = 0;
			Cmode = 1;
			mReset();
			operations = new List<Operation>();
			Fvalue = Recognition(richTextBox1.Text);
			k = 51;
			sk = k;
			sx = 0;
			sy = 0;
			sn = 2;
			dC = false;
			t = 0;
			dt = 0.1;
            dist = 1;
            pen = new Pen(Color.White);
			bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			gr = Graphics.FromImage(bitmap);
			gr.Clear(Color.FromArgb(255, 127, 0));
			//Draw();
			timer1.Start();
			textBox1.Text = "" + 255 / k;
			textBox2.Text = "" + sx;
			textBox3.Text = "" + -sy;
			textBox4.Text = "" + t;
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			if (pictureBox1.Width > 1 && pictureBox1.Height > 1)
			{
				bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
				gr = Graphics.FromImage(bitmap);
				mReset();
			}
		}

		private void Form1_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0) k *= 1.35;
			else k /= 1.35;
			textBox1.Text = "" + 255 / k;
			mReset();
			//Write();
		}

        private void button2_Click(object sender, EventArgs e)
        {
			k = 255 / Convert.ToDouble(textBox1.Text);
			sx = Convert.ToDouble(textBox2.Text);
			sy = -Convert.ToDouble(textBox3.Text);
			t = Convert.ToDouble(textBox4.Text);
			mReset();
		}

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				Lx = e.Location.X;
				Ly = e.Location.Y;
			}
			if (e.Button == MouseButtons.Left)
			{
				button1.Visible = !button1.Visible;
				button2.Visible = !button2.Visible;
				button3.Visible = !button3.Visible;
				label1.Visible = !label1.Visible;
				label2.Visible = !label2.Visible;
				label3.Visible = !label3.Visible;
				label4.Visible = !label4.Visible;
				label5.Visible = !label5.Visible;
				textBox1.Visible = !textBox1.Visible;
				textBox2.Visible = !textBox2.Visible;
				textBox3.Visible = !textBox3.Visible;
				textBox4.Visible = !textBox4.Visible;
				richTextBox1.Visible = !richTextBox1.Visible;
			}
		}

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
			if (e.Button == MouseButtons.Right)
			{
				dx = e.Location.X;
				dy = e.Location.Y;
				/*if (!dC || Cmode == 1)*/
				sx += (Lx - dx) / k;
				/*if (!dC || Cmode == 1)*/
				sy += (Ly - dy) / k;
				if (dC) CRe += (Lx - dx) / k;
				if (dC) CIm += (Ly - dy) / k;
				Lx = dx;
				Ly = dy;
				mReset();
				//Write();
				textBox2.Text = "" + sx;
				textBox3.Text = "" + -sy;
			}
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			//d++; if (d > 25) d = 0;
			label1.Text = "R: " + dist;
			Draw();
			if (animation)
			{
				t += dt;
				textBox4.Text = "" + t;
			}
		}

		private void mReset()
		{
			m = pictureBox1.Width + pictureBox1.Height;
			x = Convert.ToInt32(-1.0 * (double)pictureBox1.Width / 2.0 - (double)m / 2.0);
			y = Convert.ToInt32(-1.0 * (double)pictureBox1.Height / 2.0 - (double)m / 2.0);
		}

        private void Draw()
		{
			if (!animation)
			{
				for (int i = 0; i < 2500; i++)
				{
					if (m >= 1)// && d == 0)
					{
						if (x <= (double)pictureBox1.Width / 2.0 + (double)m)// && d == 0)
						{
							if (y <= (double)pictureBox1.Height / 2.0 + (double)m)// && d == 0)
							{
								c = new Complex(((double)x / k + sx), -((double)y / k + sy));
								//Complex c1 = new Complex(0);
								//int n = sn;
								//c1 = &c;
								//n1 = &n;
								//if (Cmode == 0) c1 = new Complex(CRe, CIm);
								//else c1 = new Complex(c);
								Color col = Calculation(c, null, 0);
								pen.Color = col;
								gr.FillRectangle(pen.Brush, Convert.ToInt32((double)x + ((double)pictureBox1.Width / 2.0 - (double)m / 2.0)), Convert.ToInt32((double)y + ((double)pictureBox1.Height / 2.0 - (double)m / 2.0)), (float)m, (float)m);
								y += Convert.ToInt32(m);
							}
							else
							{
								x += Convert.ToInt32(m);
								y = Convert.ToInt32(-1.0 * (double)pictureBox1.Height / 2.0 - (double)m / 2.0);
							}
						}
						else
						{
							//m = (m + 1) / 2 - 1;
							m /= 2;
							x = Convert.ToInt32(-1.0 * (double)pictureBox1.Width / 2.0 - (double)m / 2.0);
							y = Convert.ToInt32(-1.0 * (double)pictureBox1.Height / 2.0 - (double)m / 2.0);
						}
					}
				}
				pictureBox1.Image = bitmap;
			}
			else if (m >= 1)
			{
				m /= 300;
				Bitmap bitmap2; try { bitmap2 = new Bitmap(Convert.ToInt32(pictureBox1.Width / m), Convert.ToInt32(pictureBox1.Height / m)); }catch{ bitmap2 = new Bitmap(1, 1); };
				for (double x1 = 0; x1 < bitmap2.Width;  x1++) 
				for (double y1 = 0; y1 < bitmap2.Height; y1++)  
				{ 
					c = new Complex(((x1 - bitmap2.Width / 2.0) / k * m + sx), -((y1 - bitmap2.Height / 2.0) / k * m + sy));
					Color col = Calculation(c, null, 0);
					try { bitmap2.SetPixel(Convert.ToInt32(x1), Convert.ToInt32(y1), col); } catch { }
					//pen.Color = col;
					//gr.FillRectangle(pen.Brush, Convert.ToInt32(x1), Convert.ToInt32(y1), (float)m, (float)m);
				}
				m = 0;
				gr.DrawImage(bitmap2, 0, 0, pictureBox1.Width, pictureBox1.Height);
				pictureBox1.Image = bitmap;
				mReset();
			}
		}

		double Min(double a, double b)
        {
			if (Math.Abs(b) < Math.Abs(a) || !(Math.Abs(b) > Math.Abs(a)) && (b >= 0 || b <= 0)) a = b;
			return a;
		}

        double Max(double a, double b)
		{
			if (Math.Abs(b) > Math.Abs(a) || !(Math.Abs(b) < Math.Abs(a)) && (b >= 0 || b <= 0)) a = b;
			return a;
		}

		string Fvalue = "";
		private void button3_Click(object sender, EventArgs e)
		{
			operations = new List<Operation>();
			Fvalue = Recognition(richTextBox1.Text);
			mReset();
		}
		class Operation
		{
			public string name;
			public List<string> args;
			public Operation(string name, List<string> args)
			{
				this.name = name;
				this.args = args;
			}
		}
		List<Operation> operations;
		string Recognition(string formula)
		{
			bool multiexpression = false;
			bool And = false;
			bool Abs = false;
			bool equality = false;
			bool Sum = false;
			bool Sub = false;
			bool Mul = false;
			bool Div = false;
			bool Pow = false;
			{
				int level = 0;
				string formula1 = "";
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (formula[i] == '\n') multiexpression = true;
					if (level <= 0 && (formula[i] == '=' || formula[i] == '>' || formula[i] == '<')) equality = true;
					if (level <= 0 && !Sub && formula[i] == '+') Sum = true;
					if (level <= 0 && !Sum && formula[i] == '-') Sub = true;
					if (level <= 0 && !Div && formula[i] == '*') Mul = true;
					if (level <= 0 && !Mul && formula[i] == '/') Div = true;
					if (level <= 0 && formula[i] == '^') Pow = true;
					if (level <= 0 && formula[i] == '|') Abs = true;
					if (level <= 0 && formula[i] == '&') And = true;
					if (formula[i] != ' ')
					{
						if (formula[i] != '.') formula1 += formula[i];
						else formula1 += ',';
					}
				}
				formula = formula1;
			}

			if (multiexpression)
			{
				string formula1 = "Mex(";
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] != '\n') formula1 += formula[i];
					else formula1 += ";";
				}
				formula1 += ")";
				formula = formula1;
			}

			else if (Abs)
			{
				bool open = false;
				int level = 0;
				string formula1 = "(";
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level > 0 || formula[i] != '|') formula1 += formula[i];
					else if (formula[i] == '|')
					{
						if (!open) formula1 += "Abs(";
						else formula1 += ")";
						open = !open;
					}
				}
				formula1 += ")";
				formula = formula1;
			}

			else if (And)
			{
				int level = 0;
				string formula1 = "Max(";
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level > 0 || formula[i] != '&') formula1 += formula[i];
					else formula1 += ";";
				}
				formula1 += ")";
				formula = formula1;
			}

			else if (equality)
			{
				//bool larger = false;
				//bool less = false;
				int level = 0;
				string formula1 = "Max(";
				List<string> args = new List<string>();
				string types = "";
				string arg = "";
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level >= 0)
					{
						if (level > 0 || formula[i] != '=' && formula[i] != '>' && formula[i] != '<') arg += formula[i];
						else if (formula[i] == '=' || formula[i] == '>' || formula[i] == '<')
						{
							//if (formula[i] == '>') larger = true;
							//if (formula[i] == '<') less = true;
							//formula1 += ";";
							types += formula[i];
							args.Add(arg);
							arg = "";
						}
					}
				}
				args.Add(arg);
				for (int i = 0; i < types.Length; i++)
				{
					if (types[i] == '=') formula1 += "Eq(" + args[i] + ";" + args[i + 1] + ")";
					if (types[i] == '>') formula1 += "Lar(" + args[i] + ";" + args[i + 1] + ")";
					if (types[i] == '<') formula1 += "Les(" + args[i] + ";" + args[i + 1] + ")";
					if (i + 1 < types.Length) formula1 += ";";
				}
				formula1 += ")";
				formula = formula1;
			}

			else if (Sum || Sub)
			{
				int level = 0;
				string formula1 = ""; if (Sum) formula1 = "Sum("; if (Sub) formula1 = "Sub(";
				bool one = true;
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level > 0 || formula[i] != '+' && formula[i] != '-') formula1 += formula[i];
					else if (one) { formula1 += ";"; one = false; }
					else
					{
						if (formula[i] == '+') formula1 = "Sum(" + formula1 + ");";
						if (formula[i] == '-') formula1 = "Sub(" + formula1 + ");";
					}
				}
				formula1 += ")";
				formula = formula1;
			}

			else if (Mul || Div)
			{
				int level = 0;
				string formula1 = ""; if (Mul) formula1 = "Mul("; if (Div) formula1 = "Div(";
				bool one = true;
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level > 0 || formula[i] != '*' && formula[i] != '/') formula1 += formula[i];
					else if (one) { formula1 += ";"; one = false; }
					else
					{
						if (formula[i] == '*') formula1 = "Mul(" + formula1 + ");";
						if (formula[i] == '/') formula1 = "Div(" + formula1 + ");";
					}
				}
				formula1 += ")";
				formula = formula1;
			}

			else if (Pow)
			{
				int level = 0;
				string formula1 = "";
				bool one = true;
				for (int i = 0; i < formula.Length; i++)
				{
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level > 0 || !one || formula[i] != '^') formula1 += formula[i];
					else if (formula[i] == '^') { formula1 = "Pow(" + formula1 + ";"; one = false; }
				}
				formula1 += ")";
				formula = formula1;
			}

			while (formula != "")
            {
				int i = 0;
				string operation = "";
				while (i < formula.Length && formula[i] != '(') operation += formula[i++];
				if (i >= formula.Length) break;
				int level = 1;
				string arguments = "";
				i++;
				while (i < formula.Length && level > 0)
                {
					if (formula[i] == '(') level++;
					else if (formula[i] == ')') level--;
					if (level > 0) arguments += formula[i++];
				}
				if (i >= formula.Length || arguments == "") break;
				List<string> args = new List<string>();
				{
					string arg = "";
					level = 0;
					for (int j = 0; j < arguments.Length; j++)
					{
						if (arguments[j] == '(') level++;
						else if (arguments[j] == ')') level--;
						if (level >= 0)
						{
							if (level > 0 || arguments[j] != ';') arg += arguments[j];
							else
							{
								args.Add(arg);
								arg = "";
							}
						}

					}
					args.Add(arg);
				}
                for (int j = 0; j < args.Count; j++) args[j] = Recognition(args[j]);
				if (operation == "Sum" || operation == "Sub") for (int j = 0; j < args.Count; j++) if (args[j] == "") args[j] = "0";
				{
					int j = 0;
					for (; j < operations.Count; j++) if (operations[j].name == operation && operations[j].args == args) break;
					if (j >= operations.Count) operations.Add(new Operation(operation, args));
					string formula1 = "r" + (j);
					i++;
					while (i < formula.Length) formula1 += formula[i++];
					formula = formula1;
				}
			}
			return formula; //Sub(y; Mul(x; 2))
		}
		Complex CalculationValue(string value)
		{
			if (value == "") return new Complex(1);
			if (value[0] == 'r')
			{
				string id = ""; for (int i = 1; i < value.Length; i++) id += value[i];
				Operation op = operations[Convert.ToInt32(id)];
				List<string> sargs = op.args;
				List<Complex> args = new List<Complex>();
				foreach (string arg in sargs) args.Add(CalculationValue(arg));

				if (op.name == "") return new Complex(args[0]);
				if (op.name == "Eq")
				{
					return new Complex(args[0].Sub(args[1]).Abs());
				}
				if (op.name == "Lar") //if (larger) formula1 = "Div(Sub(" + formula1 + ";Abs(" + formula1 + "));2)";
				{
					double result = args[0].Sub(args[1]).Re;
					if (result > 0) result = 0;
					return new Complex(Math.Abs(result));
				}
				if (op.name == "Les") //if (less) formula1 = "Div(Sum(" + formula1 + ";Abs(" + formula1 + "));2)";
				{
					double result = args[0].Sub(args[1]).Re;
					if (result < 0) result = 0;
					return new Complex(Math.Abs(result));
				}
				if (op.name == "Mex")
				{
					double result = args[0].Abs();
					for (int i = 1; i < args.Count; i++) if ((args[i].Abs() < result || !(args[i].Abs() > result)) && (args[i].Abs() >= 0 || args[i].Abs() <= 0)) result = args[i].Abs();
					return new Complex(result);
				}
				if (op.name == "Complex") return new Complex(args[0].Re, args[1].Re);
				if (op.name == "Re") return new Complex(args[0].Re);
				if (op.name == "Im") return new Complex(args[0].Im);
				if (op.name == "Min" || op.name == "min") //if (Math.Abs(b) < Math.Abs(a) || !(Math.Abs(b) > Math.Abs(a)) && (b >= 0 || b <= 0)) a = b;
				{
					double result = args[0].Re;
					for (int i = 1; i < args.Count; i++) if ((args[i].Re < result || !(args[i].Re > result)) && (args[i].Re >= 0 || args[i].Re <= 0)) result = args[i].Re;
					return new Complex(result);
				}
				if (op.name == "Max" || op.name == "max")
				{
					double result = args[0].Re;
					for (int i = 1; i < args.Count; i++) if ((args[i].Re > result || !(args[i].Re < result)) && (args[i].Re >= 0 || args[i].Re <= 0)) result = args[i].Re;
					return new Complex(result);
				}
				if (op.name == "Abs" || op.name == "abs") return new Complex(args[0].Abs());
				if (op.name == "Sign" || op.name == "sign" || op.name == "sgn") return args[0].Sign();
				if (op.name == "Conj" || op.name == "conj") return args[0].Conj();
				if (op.name == "Sum" || op.name == "sum")
				{
					Complex result = new Complex(args[0]);
					for (int i = 1; i < args.Count; i++) result = result.Sum(args[i]);
					return result;
				}
				if (op.name == "Sub" || op.name == "sub") return args[0].Sub(args[1]);
				if (op.name == "Mul" || op.name == "mul")
				{
					Complex result = new Complex(args[0]);
					for (int i = 1; i < args.Count; i++) result = result.Mul(args[i]);
					return result;
				}
				if (op.name == "Div" || op.name == "div") return args[0].Div(args[1]);
				if (op.name == "Arg" || op.name == "arg" || op.name == "Angle" || op.name == "angle") return new Complex(args[0].Angle()); 
				if (op.name == "Drct" || op.name == "drct") return args[0].Drct();
				if (op.name == "Exp" || op.name == "exp") return new Complex(Math.E).Pow(args[0]);
				if (op.name == "Pow" || op.name == "pow") return args[0].Pow(args[1]);
				if (op.name == "Ln" || op.name == "ln") return args[0].Ln();
				if (op.name == "Log" || op.name == "log") return args[0].Log(args[1]);
				if (op.name == "Sin" || op.name == "sin") return args[0].Sin();
				if (op.name == "Cos" || op.name == "cos") return args[0].Cos();
				if (op.name == "Tan" || op.name == "tan" || op.name == "tg") return args[0].Tan();
				if (op.name == "Ctg" || op.name == "ctg") return new Complex(1).Div(args[0].Tan());
				if (op.name == "Sec" || op.name == "sec") return new Complex(1).Div(args[0].Cos());
				if (op.name == "Csc" || op.name == "csc" || op.name == "cosec") return new Complex(1).Div(args[0].Sin());
			}

			else if (value[0] == 'c') return c;
			else if (value[0] == 'x') return new Complex(c.Re);
			else if (value[0] == 'y') return new Complex(c.Im);
			else if (value[0] == 't') return new Complex(t);
			else if (value[0] == 'p') return new Complex(Math.PI);
			else if (value[0] == 'e') return new Complex(Math.E);
			else if (value[0] == 'i') return new Complex(0, 1);

			return new Complex(Convert.ToDouble(value));
		}

		private Color Calculation(Complex c, Complex c2, int n)
		{
			//Complex z = new Complex(c);
			//Complex c1 = new Complex(c2);
			//double i = 0;
			//for (; i < 1000; i++)
			//{
			//	if (z.Abs() == 0 && n <= 0) break;
			//	z = new Complex(c1.Sum(z.Exp(n)));
			//	//z = Complex::Sum(Complex::Mul(z, z), c1);
			//	//z = z.Sum(z.Mul(z, z), c1);
			//	if (z.Abs() > 1000) break;
			//}
			///*int r = 255 - 20*i; if (r > 255)r = 255; if (r < 0)r = 0;
			//int g = 4*i; if (g > 255)g = 255; if (g < 0)g = 0;
			//int b = 255 - 3*i*i; if (b > 255)b = 255; if (b < 0)b = 0;*/
			//if (i == 1000) return Color.FromArgb(0, 0, 0);
			//else
			//{
			//	int r = Convert.ToInt32((127 - Convert.ToInt32(255.0 * (Math.Cos(i / 5.0 + 3.1415926535)) + 0.0) / 1.0 * 1.0) / 1); if (r > 255) r = 255; if (r < 0) r = 0;
			//	int g = Convert.ToInt32((127 - Convert.ToInt32(255.0 * (Math.Cos(i / 5.0 + 5.2359877559)) + 0.0) / 1.0 * 1.0) / 1); if (g > 255) g = 255; if (g < 0) g = 0;
			//	int b = Convert.ToInt32((127 - Convert.ToInt32(255.0 * (Math.Cos(i / 5.0 + 1.0471975511)) + 0.0) / 1.0 * 1.0) / 1); if (b > 255) b = 255; if (b < 0) b = 0;
			//	return Color.FromArgb(r, g, b);
			//	//i = z.Abs();
			//	/*if (z.Abs() < 100000)
			//	{

			//	}
			//	else return Color::FromArgb(127, 127, 127);*/
			//}
			double i = 1;
			try
			{
				//для написания функции
				// i = 0 = f(x, y)
				// c.Re = x
				// c.Im = y

				i = CalculationValue(Fvalue).Abs(); //Sub(y; Sin(Mul(t; x)))

				//double i4 = Complex.Pow(Math.E, Complex.Sub(c.Im, new Complex(0, t))).Sub(c.Re).Abs(); i = Min(i, i4);
				//double i2 = Complex.Ln(new Complex(c.Re)).Sub(c.Im).Sub(new Complex(0, t % (2 * Math.PI))).Abs(); i = Min(i, i2);
                //double i3 = Complex.Ln(new Complex(c.Re)).Sub(c.Im).Sub(new Complex(0, (t + Math.PI) % (2 * Math.PI) - Math.PI)).Abs(); i = Min(i, i3);


                //i = c.Sin().Pow(2).Sum(c.Cos()).Sub(Math.Tan(t)).Abs();
                //i = c.Abs() - m / k;

                //i = Complex.Drct(t).Mul(c.Re).Cos().Abs() - c.Im;

                //i = Complex.Sub(Complex.Mul(c.Im, Complex.Drct(0 * Math.PI / 2)), Complex.Mul(c.Re, Complex.Drct(Math.PI / 2)).Cos()).Abs();
                //double i2 = c.Im - Math.Cos(c.Re); i = Min(i, i2);


                //i = c.Sin().Sub(Complex.Drct(t).Mul(2)).Abs();

                dt = 0.025; //скорость анимации

                //дополнительные приёмы:
                // if (i < 0) i = 0;  <=>  '=' -> '<' //замена равенства на сравнение
                // double iN = f(x, y); i = Min(i, iN); //для одновременной отрисовки нескольких функций

                //Библиотека готовых функций
                //i = c.Im - c.Re;								//прямая
                //i = 1 - c.Abs();								//окружность
                //i = c.Im - Math.Pow(c.Re, 2);					//парабола
                //i = c.Im - 1 / c.Re;							//гипербола
                //i = c.Im - Math.Sin(c.Re);					//синус
                //i = c.Im - Math.Abs(c.Re);					//модуль
                //i = c.Im - Math.Sign(c.Re);					//знак
                //i = c.Im % c.Re;								//остаток от деления;
                //i = 1 - c.Im * c.Re;							//гипербола без асимтоты;
                //i = c.Pow(3).Sub(2).Abs();					//кубический корень из 2
                //i = c.Im - Math.Pow(Math.Abs(c.Re), c.Re);	//тетрация
                //i = 1 - (c.Re * c.Re - c.Im * c.Im);			//гипербола
                //i = Math.Tan(Math.PI * c.Re) * Math.Tan(Math.PI * c.Im);						//сетка
                //i = 2 - (c.Sub(0.75).Abs() + c.Sum(0.75).Abs()); if (i > 0) i = 0;			//овал
                //i = 1 - c.Pow(2).Sub(1).Abs();												//знак бесконечности
                //i = 2 - (c.Pow(2).Sub(1).Abs() + c.Pow(2).Sum(1).Abs()); if (i > 0) i = 0;	//+
                //i = 2 - (c.Pow(2).Sub(0).Abs() + c.Pow(-1).Sum(0).Abs()); if (i > 0) i = 0;
                //i = 2 - (c.Sub(c.Pow(4)).Mul(1).Abs() + c.Sum(c.Pow(4)).Mul(1.5).Abs());
                //i = Math.Sin(10 * c.Im) % Math.Sin(1 * c.Re) - Math.Sin(10 * c.Re) % Math.Sin(1 * c.Im);
                //i = (Math.Abs(c.Im) - Math.Pow(Math.Abs(c.Re), Math.Tan(t))) * Math.Sign(Math.Cos(t)); if (i < 0) i = 0; //выворачивающаяся парабола
                //i = Math.Pow(2,c.Re) + 4*c.Re - c.Re - 3;
                //
                //
                //
                //Сложные функции
                //
                //
                //i = Math.Tan(Math.PI * c.Im);							//сетка
                //double i2 = Math.Tan(Math.PI * c.Re); i = Min(i, i2);
                //
                //
                //Complex z = c;							//множество Мандельброта
                //int j = 0;
                //for (; j < 100; j++)
                //{
                //    z = z.Pow(new Complex(2.01, 0)).Sum(c);
                //    if (z.Abs() > 10) break;
                //    //if (z.Abs() < 1/t) j = 100 - 1;
                //}
                //i = Math.Pow(2, 100 - j) - 1;
                //
                //
                //double an = c.Angle();									//спираль архимеда 
                //if (an < 0) an += 2 * Math.PI;
                //double an1 = an - 2 * Math.PI;
                //double an2 = an + 2 * Math.PI;
                //i = c.Abs() % (2 * Math.PI) - an;
                //double i1 = c.Abs() % (2 * Math.PI) - an1; i = Min(i, i1);
                //double i2 = c.Abs() % (2 * Math.PI) - an2; i = Min(i, i2);
                //
                //
                //Complex z = c.Mul(c.Sign());
                //i = z.Sum(new Complex(Math.Tan(t), Math.Pow(Math.Tan(t), -1))).Abs() - 1; if (i < 0) i = 0;
                //double i2 = z.Im + Math.Pow(-z.Re, -1); i = Min(i, i2);
                //
                //
                //
                //Amazing Math Graphs
                //
                //Level 1? :)
                //i = Math.Pow(c.Re, c.Im) - Math.Pow(c.Im, c.Re);				//x^y = y^x;
                //i = Math.Pow(c.Im, c.Re) * Math.Pow(c.Re, c.Im) - 0.1;		//y^x * x^y = 0.1
                //i = Math.Pow(c.Im, c.Re) - c.Re * c.Im;						//y^x = xy
                //i = Math.Pow(Math.Abs(c.Im), c.Re) - (Math.Abs(c.Re) + 2);	//|y|^x = |x| + 2
                //i = c.Re / c.Im - Math.Pow(c.Re, 3);							//x/y = x^3
                //i = c.Im - (Math.Pow(Math.E, Math.Sin(c.Re)) + c.Re);			//y = e^sin(x) + x
                //
                //Level 2
                //i = Math.Pow(c.Re, 3) + Math.Pow(c.Im, 3) - (c.Re * c.Re + c.Im);													//x^3 + y^3 = x^2 + y
                //i = Math.Pow(c.Re, 7) + Math.Pow(c.Im, 9) - (Math.Pow(c.Re, 5) + Math.Pow(c.Im, 3));								//x^7 + y^9 = X^5 + y^3
                //i = c.Im - Math.Cos(Math.Pow(c.Re, c.Re));																		//y = cos(x^x)
                //i = c.Im - Math.Sin(Math.Pow(c.Re, Math.Cos(c.Im)));																//y = sin(x^cos(y))
                //i = Math.Pow(4, Math.Log(Math.Sin(c.Im))) - Math.Tan(Math.Abs(c.Re));												//4^ln(sin(y)) = tan(|x|)
                //i = c.Im - Math.Log(Math.Sin(c.Re)); if (i < 0) i = 0;															//y < ln(sin(x))
                //i = Math.Sin(Math.Log(Math.Pow(c.Re, c.Im))); if (i > 0) i = 0;													//sin(ln(x^y)) > 0
                //i = Math.Log(Math.Pow(c.Re, c.Im)) + Math.Cos(Math.Pow(c.Im, c.Re)) - 1;											//ln(x^y) + cos(y^x) = 1
                //i = Math.Pow(c.Im, 16) - Math.Pow(c.Im, c.Re*c.Re + c.Im*c.Im) / Math.Log(Math.Pow(2, c.Re*c.Re - c.Im*c.Im));	//y^16 = y^(x^2 + y^2) / ln(2^(x^2 - y^2))
                //
                //Lavel 3
                //i = c.Im - Math.Pow(Math.E, Math.Cos(c.Im * c.Re));																															//y = e^cos(yx)
                //i = Math.Pow(7, c.Re * Math.Sin(c.Re)) + Math.Pow(c.Re, 7 * Math.Sin(c.Im)) - (c.Im * c.Re*c.Re + Math.Pow(2, c.Re * c.Im*c.Im));												//7^(x*sin(x)) + x^(7*sin(y)) = yx^2 + 2^(xy^2)
                //i = c.Re*c.Re + c.Im*c.Im - (Math.Tan(Math.Pow(Math.E, c.Re)) + Math.Abs(Math.Pow(4, c.Re) / Math.Sin(c.Im)));																//x^2 + y^2 = tan(e^x) + |4^x / sin(y)|
                //i = Math.Abs(Math.Sin(c.Re * Math.Cos(c.Re))) - 0.1;																															//|sin(x*cos(x))| = 0.1
                //i = c.Im - (Math.Pow(Math.Sin(c.Re), c.Re) + Math.Pow(Math.Cos(c.Im), c.Im));																									//y = sin(x)^x + cos(y)^y
                //i = c.Im - Math.Pow(Math.Cos(c.Re*c.Re), Math.Cos(Math.Pow(c.Re, c.Re)));																										//y = cos(x^2)^cos(x^x)
                //i = c.Im - (1 / Math.Cos(c.Re*c.Re) + 1 / Math.Sin(c.Re));																													//y = sec(x^2) + csc(x)
                //i = c.Re - Math.Sqrt(Math.Tan(c.Re * c.Im) / (Math.Pow(4, c.Im) + Math.Pow(c.Re, 4))); double i2 = c.Im - Math.Cos(Math.Pow(4, c.Re) + Math.Pow(c.Im, 4)); i = Min(i, i2);	//x = (tan(xy)/(4^y + x^4))^0.5; y = cos(4^x + y^4)
                //i = Math.Tan(Math.Pow(c.Re, c.Im)) - Math.Sin(Math.Pow(c.Re, Math.Cos(c.Im)));																								//tan(x^y) = sin(x^cos(y))
                //i = Math.Sin(Math.Pow(c.Re, Math.E)) + Math.Cos(Math.Pow(c.Im, Math.E)) - 1;																									//sin(x^e) + cos(y^e) = 1
                //i = Math.Tan(c.Re*c.Re + c.Im*c.Im) - 1;																																		//tan(x^2 + y^2) = 1
                //i = c.Re - Math.Tan(c.Im*c.Im);																																				//x = tan(y^2)
                //i = c.Re*c.Re - ((256 * Math.Pow(Math.Cos(Math.Pow(c.Im, 3)), 4) * Math.E * c.Re) % 2); if (i <= 0) i = 0;																	//x^2 <= (256 * cos(y^3)^4 * ex) mod 2

                if (c.Re > sx - m / k && c.Re < sx + m / k && c.Im < -sy + m / k && c.Im > -sy - m / k) dist = i; //вывод расстояния до функции
            }
            catch { }
			if (i == 0) return Color.FromArgb(0, 0, 0);
            else
            {
				i = Math.Abs(i);
				i = Math.Log(i,2);
				
				if (i >= 0 || i <= 0) try
				{
					int r = Convert.ToInt32((127 - Convert.ToInt32(255.0 * (Math.Cos(i / 5.0 + 3.1415926535)) + 0.0) / 1.0 * 1.0) / 1); if (r > 255) r = 255; if (r < 0) r = 0;
					int g = Convert.ToInt32((127 - Convert.ToInt32(255.0 * (Math.Cos(i / 5.0 + 5.2359877559)) + 0.0) / 1.0 * 1.0) / 1); if (g > 255) g = 255; if (g < 0) g = 0;
					int b = Convert.ToInt32((127 - Convert.ToInt32(255.0 * (Math.Cos(i / 5.0 + 1.0471975511)) + 0.0) / 1.0 * 1.0) / 1); if (b > 255) b = 255; if (b < 0) b = 0;
					return Color.FromArgb(r, g, b);
				}
				catch
				{
					return Color.FromArgb(255, 255, 255);
				}
				else
                {
					return Color.FromArgb(255, 255, 255);
				}
            }
		}

		private void button1_Click(object sender, EventArgs e)
		{
			mReset();
			animation = !animation;
			if(animation) timer1.Interval = 70;
			else timer1.Interval = 1;
		}
	}
}