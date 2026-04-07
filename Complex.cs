using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Пространство_функций
{
     class Complex
    {
		public double Re;
		public double Im;
		const double d = 4.8;

		public Complex(Complex c)
		{
			this.Re = c.Re;
			this.Im = c.Im;
		}
		public Complex(double Re, double Im)
		{
			this.Re = Re;
			this.Im = Im;
		}
		public Complex(double Re)
		{
			this.Re = Re;
			this.Im = 0;
		}

		public double Abs() // |z| модуль (абсолютное значение)
		{
			return Math.Sqrt(Re * Re + Im * Im);
		}
		public static double Abs(double a) // |z| модуль (абсолютное значение)
		{
			return Math.Abs(a);
		}
		public static double Abs(Complex a) // |z| модуль (абсолютное значение)
		{
			return Math.Sqrt(a.Re * a.Re + a.Im * a.Im);
		}

		public Complex Sign() // sign(z) направление
		{
			if (Re == 0 && Im == 0) return new Complex(1);
			else return new Complex(Re, Im).Div(new Complex(Re, Im).Abs());
		}
		public static double Sign(double a) // sign(z) направление
		{
			if (a == 0) return 1;
			else return Math.Sign(a);
		}
		public static Complex Sign(Complex a) // sign(z) направление
		{
			if (a.Re == 0 && a.Im == 0) return new Complex(1);
			else return a.Div(a.Abs());
		}

		public Complex Conj() // conj(z) комплексносопрежённое (Re(z) - Im(z))
		{
			return new Complex(Re, -Im);
		}
		public static Complex Conj(double a)  // conj(z) комплексносопрежённое (Re(z) - Im(z))
		{
			return new Complex(a);
		}
		public static Complex Conj(Complex a)  // conj(z) комплексносопрежённое (Re(z) - Im(z))
		{
			return new Complex(a.Re, -a.Im);
		}

		public Complex Sum(double b) // + сложение
		{
			return new Complex((Re + b), Im);
		}
		public Complex Sum(Complex b) // + сложение
		{
			return new Complex((Re + b.Re), (Im + b.Im));
		}
		public static double Sum(double a, double b) // + сложение
		{
			return a + b;
		}
		public static Complex Sum(Complex a, double b) // + сложение
		{
			return new Complex((a.Re + b), a.Im);
		}
		public static Complex Sum(double a, Complex b) // + сложение
		{
			return new Complex((a + b.Re), b.Im);
		}
		public static Complex Sum(Complex a, Complex b) // + сложение
		{
			return new Complex((a.Re + b.Re), (a.Im + b.Im));
		}

		public Complex Sub(double b) // - вычитание
		{
			return new Complex((Re - b), Im);
		}
		public Complex Sub(Complex b) // - вычитание
		{
			return new Complex((Re - b.Re), (Im - b.Im));
		}
		public static double Sub(double a, double b) // - вычитание
		{
			return a - b;
		}
		public static Complex Sub(Complex a, double b) // - вычитание
		{
			return new Complex((a.Re - b), a.Im);
		}
		public static Complex Sub(double a, Complex b) // - вычитание
		{
			return new Complex((a - b.Re), -b.Im);
		}
		public static Complex Sub(Complex a, Complex b) // - вычитание
		{
			return new Complex((a.Re - b.Re), (a.Im - b.Im));
		}

		public Complex Mul(double b) // * умножение
		{
			return new Complex((Re * b), (Im * b));
		}
		public Complex Mul(Complex b) // * умножение
		{
			return new Complex((Re * b.Re - Im * b.Im), (Re * b.Im + b.Re * Im));
		}
		public static double Mul(double a, double b) // * умножение
		{
			return a * b;
		}
		public static Complex Mul(Complex a, double b) // * умножение
		{
			return new Complex((a.Re * b), (a.Im * b));
		}
		public static Complex Mul(double a, Complex b) // * умножение
		{
			return new Complex((a * b.Re), (a * b.Im));
		}
		public static Complex Mul(Complex a, Complex b) // * умножение
		{
			return new Complex((a.Re * b.Re - a.Im * b.Im), (a.Re * b.Im + b.Re * a.Im));
		}

		public Complex Div(double b) // / деление
		{
			return new Complex((Re / b), (Im / b));
		}
		public Complex Div(Complex b) // / деление
		{
			Complex c = Mul(new Complex(Re, Im), b.Conj());
			return new Complex((c.Re / (b.Re * b.Re + b.Im * b.Im)), (c.Im / (b.Re * b.Re + b.Im * b.Im)));
		}
		public static double Div(double a, double b) // / деление
		{
			return a / b;
		}
		public static Complex Div(Complex a, double b) // / деление
		{
			return new Complex((a.Re / b), (a.Im / b));
		}
		public static Complex Div(double a, Complex b) // / деление
		{
			Complex c = Mul(a, b.Conj());
			return new Complex((c.Re / (b.Re * b.Re + b.Im * b.Im)), (c.Im / (b.Re * b.Re + b.Im * b.Im)));
		}
		public static Complex Div(Complex a, Complex b) // / деление
		{
			Complex c = Mul(a, b.Conj());
			return new Complex((c.Re / (b.Re * b.Re + b.Im * b.Im)), (c.Im / (b.Re * b.Re + b.Im * b.Im)));
		}

		public double Angle() // угол направления числа
		{
			return Math.Acos(Sign().Re) * Sign(Math.Asin(Sign().Im));
		}
		public static double Angle(double a) // угол направления числа
		{
			return Math.Acos(Sign(a));
		}
		public static double Angle(Complex a) // угол направления числа
		{
			return Math.Acos(Sign(a).Re) * Sign(Math.Asin(Sign(a).Im));
		}

		public Complex Drct() // числовое направление угла
		{
			return Sum(Cos(), Mul(Sin(), new Complex(0, 1)));
		}
		public static Complex Drct(double a) // числовое направление угла
		{
			return new Complex(Math.Cos(a), Math.Sin(a));
		}
		public static Complex Drct(Complex a) // числовое направление угла
		{
			return Sum(Cos(a), Mul(Sin(a), new Complex(0, 1)));
		}

		public Complex Exp(int b) // ^ возведение в степень через предел
		{
			Complex a = new Complex(Re, Im);
			Complex c = new Complex(1);
			if (b >= 0) for (int i = 0; i < b; i++)
				{
					c = c.Mul(a);
				}
			else for (int i = 0; i < -b; i++)
				{
					c = c.Div(a);
				}
			return c;
		}
		public Complex Exp(double b) // ^ возведение в степень через предел
		{
			Complex a = new Complex(Re, Im);
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
			Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
			Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
			return c3;
		}
		public Complex Exp(Complex b) // ^ возведение в степень через предел
		{
			Complex a = new Complex(Re, Im);
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
			Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
			Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
			return c3;
		}
		public static Complex Exp(Complex a, int b) // ^ возведение в степень через предел
		{
			Complex c = new Complex(1);
			if (b >= 0) for (int i = 0; i < b; i++)
				{
					c = c.Mul(a);
				}
			else for (int i = 0; i < -b; i++)
				{
					c = c.Div(a);
				}
			return c;
		}
		public static Complex Exp(double a, double b) // ^ возведение в степень через предел
		{
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(new Complex(a)) * Math.Pow(0.1, d)));
			Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
			Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
			return c3;
		}
		public static Complex Exp(double a, Complex b) // ^ возведение в степень через предел
		{
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(new Complex(a)) * Math.Pow(0.1, d)));
			Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
			Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
			return c3;
		}
		public static Complex Exp(Complex a, double b) // ^ возведение в степень через предел
		{
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
			Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
			Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
			return c3;
		}
		public static Complex Exp(Complex a, Complex b) // ^ возведение в степень через предел
		{
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
			Complex c2 = Sum(1, Mul(b, Sub(c1, 1)));
			Complex c3 = Mul(Math.Pow(Abs(c2), Math.Pow(10, d)), Drct(Angle(c2) * Math.Pow(10, d)));
			return c3;
		}

		public Complex Pow(double b) // ^ возведение в степень
		{
			Complex a = new Complex(Re, Im);
			double c1 = Math.Pow(a.Abs(), b);
			Complex c2 = Drct(a.Angle() * b);
			return Mul(c1, c2);
		}
		public Complex Pow(Complex b) // ^ возведение в степень
		{
			Complex a = new Complex(Re, Im);
			double c1 = Math.Pow(a.Abs(), b.Re);
			Complex c2 = Drct(a.Angle() * b.Re);
			Complex c3 = Drct(Math.Log(a.Abs()) * b.Im);
			double c4 = Math.Pow(Math.E, -b.Im * a.Angle());
			if (a.Re == 0 && a.Im == 0) return new Complex(0);
			else return Mul(Mul(c1, c2), Mul(c3, c4));
		}
		public static Complex Pow(double a, double b) // ^ возведение в степень
		{
			double c1 = Math.Pow(Abs(a), b);
			Complex c2 = Drct(Angle(a) * b);
			return Mul(c1, c2);
		}
		public static Complex Pow(double a, Complex b) // ^ возведение в степень
		{
			double c1 = Math.Pow(a, b.Re);
			Complex c2 = Drct(Angle(a) * b.Re);
			Complex c3 = Drct(Math.Log(Abs(a)) * b.Im);
			double c4 = Math.Pow(Math.E, -b.Im * Angle(a));
			if (a == 0) return new Complex(0);
			else return Mul(Mul(c1, c2), Mul(c3, c4));
		}
		public static Complex Pow(Complex a, double b) // ^ возведение в степень
		{
			double c1 = Math.Pow(a.Abs(), b);
			Complex c2 = Drct(a.Angle() * b);
			return Mul(c1, c2);
		}
		public static Complex Pow(Complex a, Complex b) // ^ возведение в степень
		{
			double c1 = Math.Pow(a.Abs(), b.Re);
			Complex c2 = Drct(a.Angle() * b.Re);
			Complex c3 = Drct(Math.Log(a.Abs()) * b.Im);
			double c4 = Math.Pow(Math.E, -b.Im * a.Angle());
			if (a.Re == 0 && a.Im == 0) return new Complex(0);
			else return Mul(Mul(c1, c2), Mul(c3, c4));
		}

		public Complex Ln() // ln() натуральный логарифм через предел
		{
			Complex a = new Complex(Re, Im);
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
			Complex c2 = Mul(Math.Pow(10, d), Sub(c1, 1));

			return c2;
		}
		public static Complex Ln(double a) // ln() натуральный логарифм через предел
		{
			return new Complex(Math.Log(a));
		}
		public static Complex Ln(Complex a) // ln() натуральный логарифм через предел
		{
			Complex c1 = Mul(Math.Pow(Abs(a), Math.Pow(0.1, d)), Drct(Angle(a) * Math.Pow(0.1, d)));
			Complex c2 = Mul(Math.Pow(10, d), Sub(c1, 1));

			return c2;
		}

		public Complex Log(double a) // log() логарифм
		{
			Complex b = new Complex(Re, Im);
			return Div(Ln(b), Ln(a));
		}
		public Complex Log(Complex a) // log() логарифм
		{
			Complex b = new Complex(Re, Im);
			return Div(Ln(b), Ln(a));
		}
		public static Complex Log(double a, double b) // log() логарифм
		{
			return Div(Ln(b), Ln(a));
		}
		public static Complex Log(double a, Complex b) // log() логарифм
		{
			return Div(Ln(b), Ln(a));
		}
		public static Complex Log(Complex a, double b) // log() логарифм
		{
			return Div(Ln(b), Ln(a));
		}
		public static Complex Log(Complex a, Complex b) // log() логарифм
		{
			return Div(Ln(b), Ln(a));
		}

		public Complex Sin() // sin() синус
		{
			Complex a = new Complex(Re, Im);
			return Div(Sub(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), new Complex(0, 2));
		}
		public static Complex Sin(double a) // sin() синус
		{
			return new Complex(Math.Sin(a));
		}
		public static Complex Sin(Complex a) // sin() синус
		{
			return Div(Sub(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), new Complex(0, 2));
		}

		public Complex Cos() // cos() косинус
		{
			Complex a = new Complex(Re, Im);
			return Div(Sum(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), 2);
		}
		public static Complex Cos(double a) // cos() косинус
		{
			return new Complex(Math.Cos(a));
		}
		public static Complex Cos(Complex a) // cos() косинус
		{
			return Div(Sum(Pow(Math.E, Mul(a, new Complex(0, 1))), Pow(Math.E, Mul(a, new Complex(0, -1)))), 2);
		}
		public Complex Tan() // tg() тангенс
		{
			Complex a = new Complex(Re, Im);
			return Div(Sin(a), Cos(a));
		}
		public static Complex Tan(double a) // tg() тангенс
		{
			return new Complex(Math.Tan(a));
		}
		public static Complex Tan(Complex a) // tg() тангенс
		{
			return Div(Sin(a), Cos(a));
		}
	}
}