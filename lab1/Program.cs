﻿using System;

namespace lab1
{
    abstract class Vehicle
    {
        private const string typeInfoText = "Vehicles are machinery wich help us transport ourselves anywhere we want.\nBut you can't just swim across the ocean in automobile, so there are alot of types of vehicles availaible.";
        public string model;
        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                if (value.Length < 4)
                {
                    Console.WriteLine("Model name is too short");
                }
                else
                {
                    model = value;
                }
            }
        }
        protected string introduction;
        protected string engine;
        protected float velocity;
        protected double weight;
        public virtual void printInfo()
        {
            Console.WriteLine("This is " + this.model + ";\nIt was first introduced in " + this.introduction + "\nIt has " + this.engine + " engine, can pick up speed of " + this.velocity + "\nIt also is " + this.weight + " kg of weight\n");
        }
        public virtual void printBasicInfo()
        {
            Console.WriteLine("This is " + this.model + ";\nIt was first introduced in " + this.introduction + "\n");
        }
        public static void typeInfo()
        {
            Console.WriteLine(typeInfoText);
        }
    }
    abstract class Car : Vehicle
    {
        private const string typeInfoText = "Cars are generally on land vehicles with some wheels. There are some subtypes, like automobile or a truck.";

        private static bool initialized = false;
        static Car()
        {
            initialized = true;
        }
        static public void isInitialized()
        {
            Console.WriteLine("Were any cars nentioned? " + initialized);
        }
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
        new public static void typeInfo()
        {
            Console.WriteLine(typeInfoText);
        }
    }
    class Automobile : Car
    {
        private const string typeInfoText = "Automobiles are very fast. Definetely faster than trucks fully loaded with bananas :)\nThe most common type of vehicle seen in towns.";
        private int passengerSits;
        private readonly bool isRacing;
        public Automobile()
        {
            this.model = "Default Model";
            this.introduction = "19XX";
            this.engine = "Default Engine";
            this.velocity = 0f;
            this.weight = 0;
            this.wheels = 0;
            this.snowTire = false;
            this.passengerSits = 0;
            isRacing = false;
        }
        public Automobile(string model, string introduction, string engine, float velocity, double weight, int wheels, bool snowTire, int passengerSits, bool isRacingInput)
        {
            this.model = model;
            this.introduction = introduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.wheels = wheels;
            this.snowTire = snowTire;
            this.passengerSits = passengerSits;
            isRacing = isRacingInput;
        }
        public Automobile(string model, string introduction)
        {
            this.model = model;
            this.introduction = introduction;
            this.engine = "Default Engine";
            this.velocity = 0f;
            this.weight = 0;
            this.wheels = 4;
            this.snowTire = false;
            this.passengerSits = 0;
            isRacing = false;
        }
        public override void printInfo()
        {
            Console.WriteLine("[The model of automobile is " + this.model + ";\nIt was first introduced in " + this.introduction + "\nAutomobile has engine " + this.engine + "\nVelocity is " + this.velocity + "\nAuto is " + this.weight + " kg of weight.\nIt also has " + this.wheels + " wheels.\nAvalaibilty of SnowTires is " + this.snowTire + ".\nThere are also " + this.passengerSits + " passenger sits.\nCan it race? " + this.isRacing + "]");
        }
        public static void typeInfo()
        {
            Console.WriteLine(typeInfoText);
        }

    }
    class Truck : Car
    {
        private const string typeInfoText = "Trucks are useful for transporting things.\n If you think, trucks are what make us a modern people: we get food, furniture, ordered goods via this trnsport.";
        private double coachbuilderSize;
        public Truck()
        {
            this.model = "Default Truck";
            this.introduction = "19XX";
            this.engine = "Basic Truck Engine";
            this.velocity = 60.3f;
            this.weight = 1000;
            this.wheels = 6;
            this.snowTire = false;
            this.coachbuilderSize = 13.5;
        }
        public Truck(string model, string introduction, string engine, float velocity, double weight, int wheels, bool snowTire, double coachbuilderSize)
        {
            this.model = model;
            this.introduction = introduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.wheels = wheels;
            this.snowTire = snowTire;
            this.coachbuilderSize = coachbuilderSize;
        }
        public override void printInfo()
        {
            Console.WriteLine("[This truck is " + this.model + ";\nIt was first introduced in " + this.introduction + "\nTruck has engine " + this.engine + "\nVelocity is " + this.velocity + "\nTruck is " + this.weight + " kg of weight.\nIt also has " + this.wheels + " wheels.\nAvalaibilty of SnowTires is " + this.snowTire + ".\nCoachbuilder size is " + this.coachbuilderSize + "]");
        }
        public static void typeInfo()
        {
            Console.WriteLine(typeInfoText);
        }
    }
    class Airplane : Vehicle
    {
        private const string typeInfoText = "Planes are cool. They can fly up in the sky.\n";
        private float flyingHeight;
        private readonly bool isWarplane;
        private double wingSize;
        public Airplane() { }
        public Airplane(string model, string introduction, string engine, float velocity, double weight, float flyingHeight, bool isWarplaneInput, double wingSize)
        {
            this.model = model;
            this.introduction = introduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.flyingHeight = flyingHeight;
            isWarplane = isWarplaneInput;
            this.wingSize = wingSize;
        }

        private Airplane(string clone, string model, string introduction, string engine, float velocity, double weight, float flyingHeight, bool isWarplaneInput, double wingSize)
        {
            this.model = model;
            this.introduction = introduction;
            this.engine = engine;
            this.velocity = velocity;
            this.weight = weight;
            this.flyingHeight = flyingHeight;
            isWarplane = isWarplaneInput;
            this.wingSize = wingSize;
        }
        public Airplane(string model)
        {
            this.model = model;
            this.introduction = "19XX year";
            this.engine = "Default Engine";
            this.velocity = 15f;
            this.weight = 34.5;
            this.flyingHeight = 100.3f;
            isWarplane = false;
            this.wingSize = 5;
        }
        public static Airplane Clone(Airplane planeSource, string newname)
        {
            Airplane copyPlane = new Airplane("clone", newname, planeSource.introduction, planeSource.engine, planeSource.velocity, planeSource.weight, planeSource.flyingHeight, planeSource.isWarplane, planeSource.wingSize);
            return copyPlane;
        }

        public override void printInfo()
        {
            Console.WriteLine("[This plane is " + this.model + ".\nIt was first introduced in " + this.introduction + ".\nAirplane has " + this.engine + " engine, can fly with speed of " + this.velocity + " m/s.\nIt also is " + this.weight + " kg of weight.\nFlying height is " + this.flyingHeight + ".\nCan it be warplane? " + this.isWarplane + ".\nThe wings are " + this.wingSize + " meters in size.]\n");

        }
        public override void printBasicInfo()
        {
            Console.WriteLine("This plane is " + this.model + ";\nIt was first introduced in " + this.introduction + "\n");

        }
        public static void typeInfo()
        {
            Console.WriteLine(typeInfoText);
        }
    }
    class Ship : Vehicle
    {
        private const string typeInfoText = "The most efficient way to cross big waters is... Swimming! Ships can do this very well, so we use them to sail.\n";
        private int diveDepth;
        public Ship()
        {
            this.model = "Default ship";
            this.introduction = "19XX";
            this.engine = "Default Engine";
            this.velocity = 34f;
            this.weight = 34.6;
            this.diveDepth = 10;
        }

        public Ship(string model, string introduction, string engine, float velocity, double weight, int diveDepth)
        {
            this.model = "Default ship";
            this.introduction = "19XX";
            this.engine = "Default Engine";
            this.velocity = 34f;
            this.weight = 34.6;
            this.diveDepth = 10;
        }

        public override void printInfo()
        {
            Console.WriteLine("[This ship is " + this.model + ".\nIt was first introduced in " + this.introduction + ".\nAirplane has " + this.engine + " engine, can steer with speed of " + this.velocity + " m/s.\nIt also is " + this.weight + " kg of weight.\nIt's diving depth is " + this.diveDepth);

        }
        new public static void typeInfo()
        {
            Console.WriteLine(typeInfoText);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car.isInitialized(); // Checks is class used
            Console.WriteLine("\n===Calling static method to get const info about class===\n");
            Automobile.typeInfo(); // get info about class without object
            Vehicle.typeInfo(); // virtual method of a base abstract class
            Automobile myCar1 = new Automobile("Tesla X", "November 2017", "Super Duper Engine", 34.5f, 560, 4, true, 4, true); // create Automobile class object
            Console.WriteLine("\n===We print all info with overrided method===\n");
            myCar1.printInfo(); // Now printing only base info: model and introduction date
            Console.WriteLine("\n===Now we print only basic info===\n");
            myCar1.printBasicInfo();
            Console.WriteLine("\n===Car class has property \"Popularity\", which checks if assigned value is too low===\n"); // 
            Console.WriteLine("Car popularity is " + Car.Popularity);
            Console.WriteLine("\n===Now we try to set low popularity===\n");
            Car.Popularity = 5;
            Console.WriteLine("\n===Which leads to warning and changes nothing in the end===\n");
            Console.WriteLine("Car popularity is " + Car.Popularity);
            Console.WriteLine("\n===Now we set it properly===\n");
            Car.Popularity = 50;
            Console.WriteLine("Car popularity is " + Car.Popularity);
            Console.WriteLine("\n===Creating instance of Airplane===\n");
            Airplane plane = new Airplane("Chaika");
            plane.printInfo();
            Console.WriteLine("\n===With private constructor and static method we can make copy of object and add some changes===\n");
            Airplane planeCopy = Airplane.Clone(plane, "Lastochka");
            planeCopy.printInfo();
            plane.Model = "A";
            Console.WriteLine(plane.Model);
        }
    }
}
