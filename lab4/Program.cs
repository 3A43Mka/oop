using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

// code in general:
// elegant and productive
// simple
// readable and compatible
// testable
namespace lab3
{
    // classes must be compact
    // class name must display what it is
    // have less than 10 public methods
    // must complement each other
    // loose coupling

    public class TrafficSign<T> // More clear name: changed name from Sign to TrafficSign
    {
        public string title { get; set; }
        public T distance { get; set; }
    }
    // names are clear and consistent
    // no ambiguity
    // abbreviations are bad
    [Serializable]
    public class Person : IComparable, IDisposable
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        // functions are:
        // as short as possible
        // the lesser arguments is better
        public void Dispose()
        {
            Console.WriteLine(this.firstName + " is being disposed");
            GC.SuppressFinalize(this);
        }


        ~Person()
        {
            if (this.firstName == "Bear")
            {
                Console.WriteLine("Bear was resurrected");
                GC.ReRegisterForFinalize(this);
            }
            else
            {
                Console.WriteLine(this.firstName + " is on his way to be destructed");
            }
        }
        public int CompareTo(object obj)
        {
            Person person = obj as Person; // p to person, o to obj
            if (person != null)
                return this.lastName.CompareTo(person.lastName);
            else
                throw new Exception("Can't compare");
        }
    }
    public class People : IEnumerable
    {
        private Person[] people;
        public Person this[string index]
        {
            get
            {
                for (int i = 0; i < people.Length; i++)
                {
                    if (index == people[i].firstName)
                    {
                        return people[i];
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < people.Length; i++)
                {
                    if (index == people[i].firstName)
                    {
                        people[i] = value;
                        return;
                    }
                }
                int ind = people.Length;
                people[ind] = value;
            }
        }
        public People(Person[] personArray) // pArray to personArray
        {
            people = new Person[personArray.Length];

            for (int i = 0; i < personArray.Length; i++)
            {
                people[i] = personArray[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(people);
        }
    }
    public class PeopleEnum : IEnumerator
    {
        public Person[] people;
        int position = -1;
        public PeopleEnum(Person[] list)
        {
            people = list;
        }
        public bool MoveNext()
        {
            position++;
            return (position < people.Length);
        }
        public void Reset()
        {
            position = -1;
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public Person Current
        {
            get
            {
                try
                {
                    return people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public static class PeopleExtension
    {
        public static void PrintAllPeople(this People people) // method name is clear, displays what it does
        {
            foreach (Person person in people) // changed p to person to be more clear
                Console.WriteLine("MISTER " + person.firstName + " " + person.lastName);
        }
    }
    interface IInformative
    {
        void PrintAllInfo();
        void PrintBasicInfo();
    }
    interface IChangeable
    {
        void Rename(string newName);
        void ChangeEngine(string newEngine);
    }
    interface ISafeRiding
    {
        void Ride();
    }
    interface IFastRiding
    {
        void Ride();
    }
    class AutomobileException : ArgumentException
    {
        public int Value { get; }
        public AutomobileException(string message, int val) : base(message)
        {
            Value = val;
        }
    }
    abstract class Vehicle : IInformative
    {
        private const string typeDescription = "Vehicles are machinery wich help us transport ourselves anywhere we want.\nBut you can't just swim across the ocean in automobile, so there are alot of types of vehicles availaible."; // rename from typeInfoText to typeDescription
        public string modelName; // changed from model to modelName to be more clear
        public string ModelName 
        {
            get
            {
                return modelName;
            }
            set
            {
                if (value.Length < 4)
                {
                    Console.WriteLine("Model name is too short");
                }
                else
                {
                    modelName = value;
                }
            }
        }
        protected string dateOfIntroduction; // changed from introduction to dateOfIntroduction to be more clear
        protected string engine;
        protected float velocity;
        protected double weight;
        public abstract void PrintAllInfo(); // Clear name: from PrintInfo to PrintAllInfo
        public abstract void PrintBasicInfo(); // Already was ok
        public static void TypeInfo() // Changed name from TypeInfo to PrintTypeInfo to be more clear
        {
            Console.WriteLine(typeDescription);
        }
    }
    abstract class Car : Vehicle
    {
        private const string typeDescription = "Cars are generally on land vehicles with some wheels. There are some subtypes, like automobile or a truck.";
        private static bool initialized = false;
        static Car()
        {
            initialized = true;
        }
        static public void IsInitialized()
        {
            Console.WriteLine("Were any cars nentioned? " + initialized);
        }
        protected int fuel;
        protected int wheels;
        protected bool snowTire;
        protected static int popularity = 80;
        public static int Popularity
        {
            get
            {
                return popularity;
            }
            set
            {
                if (value > 10)
                {
                    popularity = value;
                }
                else if (value >= 100)
                {
                    Console.WriteLine("Popularity can't be higher than 100. Try something different");
                }
                else
                {
                    Console.WriteLine("Cars can't be THIS unpopular. Try something different");
                }
            }
        }
        new public static void TypeInfo()
        {
            Console.WriteLine(typeDescription);
        }
    }
    class Automobile : Car, IChangeable, ISafeRiding, IFastRiding
    {
        public delegate void CarHandler(string message, int fuel, Automobile car);
        public event CarHandler Notify;
        private const string typeDescription = "Automobiles are very fast. Definetely faster than trucks fully loaded with bananas :)\nThe most common type of vehicle seen in towns.";
        private int passengerSits;
        private readonly bool isRacing;
        private ArrayList passengers;
        public Automobile()
        {
            this.modelName = "Default Model";
            this.dateOfIntroduction = "19XX";
            this.engine = "Default Engine";
            this.velocity = 0f;
            this.weight = 0;
            this.wheels = 0;
            this.snowTire = false;
            this.passengerSits = 0;
            this.fuel = 0;
            isRacing = false;
            this.passengers = new ArrayList();
        }
        public Automobile(string modelName, string dateOfIntroduction, string engine, float velocity, double weight, int wheels, bool snowTire, int passengerSits, bool isRacingInput, int fuel)
        {
            this.modelName = modelName;
            this.dateOfIntroduction = dateOfIntroduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.wheels = wheels;
            this.snowTire = snowTire;
            this.passengerSits = passengerSits;
            isRacing = isRacingInput;
            this.fuel = fuel;
            this.passengers = new ArrayList();
        }
        public Automobile(string modelName, string dateOfIntroduction)
        {
            this.modelName = modelName;
            this.dateOfIntroduction = dateOfIntroduction;
            this.engine = "Default Engine";
            this.velocity = 0f;
            this.weight = 0;
            this.wheels = 4;
            this.snowTire = false;
            this.passengerSits = 0;
            isRacing = false;
            this.fuel = 0;
            this.passengers = new ArrayList();
        }
        public override void PrintAllInfo()
        {
            Console.WriteLine("[The model of automobile is " + this.modelName + ";\nIt was first introduced in " + this.dateOfIntroduction + "\nAutomobile has engine " + this.engine + "\nVelocity is " + this.velocity + "\nAuto is " + this.weight + " kg of weight.\nIt also has " + this.wheels + " wheels.\nAvalaibilty of SnowTires is " + this.snowTire + ".\nThere are also " + this.passengerSits + " passenger sits.\nCan it race? " + this.isRacing + "Fuel level is " + this.fuel + "]");
        }
        public override void PrintBasicInfo()
        {
            Console.WriteLine("This automobile is " + this.modelName + ";\nIt was first introduced in " + this.dateOfIntroduction + "\n");
        }
        new public static void TypeInfo()
        {
            Console.WriteLine(typeDescription);
        }
        public void Rename(string newName)
        {
            ModelName = newName;
        }
        public void ChangeEngine(string newEngine)
        {
            this.engine = newEngine;
        }
        public void GasUp(int gas)
        {
            this.fuel += gas;
            Notify?.Invoke("Car was gassed up by " + gas + ". Fuel level is now at " + this.fuel, this.fuel, this);
        }
        void ISafeRiding.Ride()
        {
            if (this.fuel >= 10)
            {
                this.fuel -= 10;
                Notify?.Invoke("The car " + modelName + " rides out safely! Fuel level is now at " + this.fuel, this.fuel, this);
            }
            else
            {
                Notify?.Invoke("The car " + modelName + " has no fuel for action " + this.fuel, this.fuel, this);
            }
        }
        void IFastRiding.Ride()
        {
            if (this.fuel >= 30)
            {
                this.fuel -= 30;
                Notify?.Invoke("The car " + modelName + " rides out very fast! Watch out for the fuel, it's level is now at " + this.fuel, this.fuel, this);
            }
            else
            {
                throw new AutomobileException("The car has no gas for this action!", this.fuel);
            }
        }
        public void AddPassenger(string passengerName)
        {
            this.passengers.Add(passengerName);
            Console.WriteLine(passengerName + " was added to " + this.modelName);
        }
        public void RemovePassenger(string passengerName)
        {
            if (this.passengers.Contains(passengerName))
            {
                this.passengers.Remove(passengerName);
                Console.WriteLine(passengerName + ", get out of the " + this.modelName);
            }
            else
            {
                Console.WriteLine(this.modelName + " has no " + passengerName + " in it!");
            }
        }
        public bool CheckPassenger(string passengerName)
        {
            return this.passengers.Contains(passengerName);
        }
        public void PrintPassengers()
        {
            if (this.passengers.Count == 0)
            {
                Console.WriteLine(this.modelName + " has no passengers!");
                return;
            }
            int counter = 0;
            Console.WriteLine(this.modelName + " has such passengers:");
            foreach (string passenger in this.passengers)
            {
                Console.WriteLine("[" + counter + "] " + passenger);
                counter++;
            }
        }
    }
    class Truck : Car
    {
        private const string typeDescription = "Trucks are useful for transporting things.\n If you think, trucks are what make us a modern people: we get food, furniture, ordered goods via this trnsport.";
        private double coachbuilderSize;
        public Truck()
        {
            this.modelName = "Default Truck";
            this.dateOfIntroduction = "19XX";
            this.engine = "Basic Truck Engine";
            this.velocity = 60.3f;
            this.weight = 1000;
            this.wheels = 6;
            this.snowTire = false;
            this.coachbuilderSize = 13.5;
        }
        public Truck(string modelName, string dateOfIntroduction, string engine, float velocity, double weight, int wheels, bool snowTire, double coachbuilderSize)
        {
            this.modelName = modelName;
            this.dateOfIntroduction = dateOfIntroduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.wheels = wheels;
            this.snowTire = snowTire;
            this.coachbuilderSize = coachbuilderSize;
        }
        public override void PrintAllInfo()
        {
            Console.WriteLine("[This truck is " + this.modelName + ";\nIt was first introduced in " + this.dateOfIntroduction + "\nTruck has engine " + this.engine + "\nVelocity is " + this.velocity + "\nTruck is " + this.weight + " kg of weight.\nIt also has " + this.wheels + " wheels.\nAvalaibilty of SnowTires is " + this.snowTire + ".\nCoachbuilder size is " + this.coachbuilderSize + "]");
        }
        public override void PrintBasicInfo()
        {
            Console.WriteLine("This truck is " + this.modelName + ";\nIt was first introduced in " + this.dateOfIntroduction + "\n");
        }
        new public static void TypeInfo()
        {
            Console.WriteLine(typeDescription);
        }
    }
    class Airplane : Vehicle
    {
        private const string typeDescription = "Planes are cool. They can fly up in the sky.\n";
        private float flyingHeight;
        private readonly bool isWarplane;
        private double wingSize;
        public Airplane() { }
        public Airplane(string modelName, string dateOfIntroduction, string engine, float velocity, double weight, float flyingHeight, bool isWarplaneInput, double wingSize)
        {
            this.modelName = modelName;
            this.dateOfIntroduction = dateOfIntroduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.flyingHeight = flyingHeight;
            isWarplane = isWarplaneInput;
            this.wingSize = wingSize;
        }
        private Airplane(string clone, string modelName, string dateOfIntroduction, string engine, float velocity, double weight, float flyingHeight, bool isWarplaneInput, double wingSize)
        {
            this.modelName = modelName;
            this.dateOfIntroduction = dateOfIntroduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.flyingHeight = flyingHeight;
            isWarplane = isWarplaneInput;
            this.wingSize = wingSize;
        }
        public Airplane(string modelName)
        {
            this.modelName = modelName;
            this.dateOfIntroduction = "19XX year";
            this.engine = "Default Engine";
            this.velocity = 15f;
            this.weight = 34.5;
            this.flyingHeight = 100.3f;
            isWarplane = false;
            this.wingSize = 5;
        }
        public static Airplane Clone(Airplane planeSource, string newname)
        {
            Airplane copyPlane = new Airplane("clone", newname, planeSource.dateOfIntroduction, planeSource.engine, planeSource.velocity, planeSource.weight, planeSource.flyingHeight, planeSource.isWarplane, planeSource.wingSize);
            return copyPlane;
        }

        public override void PrintAllInfo()
        {
            Console.WriteLine("[This plane is " + this.modelName + ".\nIt was first introduced in " + this.dateOfIntroduction + ".\nAirplane has " + this.engine + " engine, can fly with speed of " + this.velocity + " m/s.\nIt also is " + this.weight + " kg of weight.\nFlying height is " + this.flyingHeight + ".\nCan it be warplane? " + this.isWarplane + ".\nThe wings are " + this.wingSize + " meters in size.]\n");
        }
        public override void PrintBasicInfo()
        {
            Console.WriteLine("This plane is " + this.modelName + ";\nIt was first introduced in " + this.dateOfIntroduction + "\n");
        }
        new public static void TypeInfo()
        {
            Console.WriteLine(typeDescription);
        }
    }
    class Ship : Vehicle
    {
        private const string typeDescription = "The most efficient way to cross big waters is... Swimming! Ships can do this very well, so we use them to sail.\n";
        private int diveDepth;
        public Ship()
        {
            this.modelName = "Default ship";
            this.dateOfIntroduction = "19XX";
            this.engine = "Default Engine";
            this.velocity = 34f;
            this.weight = 34.6;
            this.diveDepth = 10;
        }
        public Ship(string modelName, string dateOfIntroduction, string engine, float velocity, double weight, int diveDepth)
        {
            this.modelName = "Default ship";
            this.dateOfIntroduction = "19XX";
            this.engine = "Default Engine";
            this.velocity = 34f;
            this.weight = 34.6;
            this.diveDepth = 10;
        }

        public override void PrintAllInfo()
        {
            Console.WriteLine("[This ship is " + this.modelName + ".\nIt was first introduced in " + this.dateOfIntroduction + ".\nAirplane has " + this.engine + " engine, can steer with speed of " + this.velocity + " m/s.\nIt also is " + this.weight + " kg of weight.\nIt's diving depth is " + this.diveDepth);

        }
        public override void PrintBasicInfo()
        {
            Console.WriteLine("This ship is " + this.modelName + ";\nIt was first introduced in " + this.dateOfIntroduction + "\n");

        }
        new public static void TypeInfo()
        {
            Console.WriteLine(typeDescription);
        }
    }


    class Program
    {
        delegate void CarHandler(string message, Automobile car);
        static WeakReference weakGuy;
        static void Main(string[] args)
        {
            Console.WriteLine("Total memory is " + GC.GetTotalMemory(false));
            CreateWeakReferencePerson("Roman", "Ivanov");
            if (weakGuy.IsAlive)
            {
                Console.WriteLine("He is alive");
            }
            else
            {
                Console.WriteLine("He is dead");

            }
            Person mainGuy = new Person { firstName = "Main", lastName = "Guy" };
            Console.WriteLine("Generation of Bear Grills is: " + GC.GetGeneration(mainGuy));
            Console.WriteLine("Total memory is " + GC.GetTotalMemory(false));
            CreatePerson("Sam", "Porter");
            CreatePerson("Bear", "Grills");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (weakGuy.IsAlive)
            {
                Console.WriteLine("He is alive");
            }
            else
            {
                Console.WriteLine("He is dead");

            }
            Console.WriteLine("Total memory is " + GC.GetTotalMemory(false));
            CreatePersonThenDispose("Oleg", "Ptichkin");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Generation of Bear Grills is: " + GC.GetGeneration(mainGuy));
        }
        private static void CreatePerson(string firstName, string lastName) // renamed from IntroducePerson to CreatePerson, added args to control name
        {
            Person person = new Person { firstName = firstName, lastName = lastName }; // p to person
            Console.WriteLine("Hi, im " + person.firstName + " " + person.lastName);
        }
        private static void CreateWeakReferencePerson(string firstName, string lastName) // renamed from IntroduceWeak to CreateWeakReferencePerson, added args to control name
        {
            weakGuy = new WeakReference(new Person { firstName = firstName, lastName = lastName });
            Console.WriteLine("Hi, im " + firstName + " " + lastName);
        }
        private static void CreatePersonThenDispose(string firstName, string lastName)
        {
            Person person = new Person { firstName = firstName, lastName = lastName }; // p to person, added args to control name
            Console.WriteLine("Hi, im " + person.firstName + " " + person.lastName);
            person.Dispose();
        }
        private static void CarEvent(string message, int fuel, Automobile car)
        {
            Console.WriteLine(message);
            if (fuel < 2)
            {
                car.GasUp(30);
            }
        }
        static void OperationWithCar(Automobile car, Action<Automobile> op)
        {
            if (car.modelName != "")
                op(car);
        }
        static void ShoutOutModel(Automobile car)
        {
            Console.WriteLine("Hey, " + car.modelName + "!");
        }
        static string GetUpperModelName(string modelName, Func<string, string> retF)
        {
            return retF(modelName);
        }
        static string ModelNameToUpperCase(string modelName)
        {
            return modelName.ToUpper() + " [MODIFIED]";
        }
    }
}
