﻿namespace Actors
{
    public class EssenceAffinity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public EssenceAffinity(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
