using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework6_Csh
{
    internal class Adversary
    {
        // Attributes
        protected int N { get; private set; }
        protected int M { get; private set; }
        protected float P { get; private set; }

        // This list represents the history of all N attacks for all M systems
        protected List<List<int>> lineChartValuesChart1 { get; private set; }


        // Constructor
        public Adversary(int m, int n, float p)
        {
            M = m;
            N = n;
            P = p;
            lineChartValuesChart1 = new List<List<int>>();
        }

        // Method to generate attacks
        public bool generateAttacks()
        {
            Random random = new Random();

            for (int i = 0; i < M; i++)
            {
                List<int> valuesChart1 = new List<int>();
                List<int> valuesChart2 = new List<int>();

                for (int j = 0; j < N; j++)
                {
                    // Generate random from (0, 1]
                    float randomNumber = (float)random.NextDouble();

                    if (randomNumber > P)
                    {
                        // Attacck success
                        //Debug.WriteLine("System " + i + ": Attack" + j + " succeeded");
                        valuesChart1.Add(-1);
                        valuesChart2.Add(1);
                    }
                    else
                    {
                        // Attacco failed
                        //Debug.WriteLine("System " + i + ": Attack" + j + " failed");
                        valuesChart1.Add(+1);
                        valuesChart2.Add(0);
                    }
                }
                lineChartValuesChart1.Add(valuesChart1);
            }


            return true;
        }


        public List<Dictionary<int, int>> createCompleteHistogramData(List<List<int>> values, int S) {
            List<Dictionary<int, int>> list = new List<Dictionary<int, int>>();

            for (int k = 2; k <= 10; k++) { 
                list.Add(createHistoDistrib(values, S, k*10));
            }

            return list;

        }

        //ritorna un dizionario che indica per ogni sistema, per una certa S e una carta P se il sistema è sicuro o no
        private Dictionary<int, int> createHistoDistrib(List<List<int>> values, int S, int P)
        {

            Dictionary<int, int> finalValues = new Dictionary<int, int>();

            for (int i = 0; i < values.Count; i++)
            {
                int sum = 0;
                for (int s = 0; s < values[i].Count; s++)
                {
                    sum += values[i][s];
                    // Check if I reached S or P
                    if (sum == P)
                    {
                        //dire che ho è non sicuro
                        finalValues.Add(i, 0);
                        //uscire dal for
                        break;
                    }
                    else if (sum == S) {
                        //dire che ho è sicuro
                        finalValues.Add(i, 1);
                        //uscire dal for
                        break;
                    }
                }
                //Debug.WriteLine("   System" + i + " final value = " + sum);
            }

            return finalValues;
        }

        public List<List<int>> GetLineChart1AttackList()
        {
            return lineChartValuesChart1;
        }

    }
}
