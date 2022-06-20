using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMethod
{
    public class Massive
    {
        public double[] ArrayDouble { get; set; }
        public int Length { get; set; }
        public Massive(double[] array)
        {
            ArrayDouble = new double[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                ArrayDouble[i] = array[i];
            }
            Length = array.Length;
        }
        public Massive(int n)
        {
            ArrayDouble = new double[n];
            Length =n;
        }
        public Massive(Massive array)
        {
            Length = array.Length;
            ArrayDouble = new double[Length];
            for (int i = 0; i < Length; i++)
                ArrayDouble[i] = array[i];
        }
        public double this[int i]
        {
            get
            {
                return ArrayDouble[i];
            }
            set
            {

                ArrayDouble[i] = value;
            }
        }

        public static Massive operator +(Massive array1, Massive array2)
        {
            try
            {
                if (array1.Length != array2.Length)
                    throw new ArgumentException("Длина векторов должна быть одинаковой");
                var result = new Massive(array1.Length);
                for (int i = 0; i < array1.Length; i++)
                {
                    result[i] = array1[i] + array2[i];
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            return new Massive(0);
        }public static Massive operator -(Massive array1, Massive array2)
        {
            try
            {
                if (array1.Length != array2.Length)
                    throw new ArgumentException("Длина векторов должна быть одинаковой");
                var result = new Massive(array1.Length);
                for (int i = 0; i < array1.Length; i++)
                {
                    result[i] = array1[i] - array2[i];
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            return new Massive(0);
        }
        public static Massive operator *(Massive array1, double number)
        {
            try
            {
                var result = new Massive(array1.Length);
                for (int i = 0; i < array1.Length; i++)
                {
                    result[i] = array1[i] * number;
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            return new Massive(0);
        }
        public static Massive operator /(Massive array, double b)
        {
            var temp = new Massive(array);
            for (int i = 0; i < temp.Length; i++)
                temp[i] = array[i] / b;
            return temp;
        }

    }
}
