using System;

namespace WeatherToday.Core.Models.BO.Base
{
    public abstract class BaseBO<BOType> : BaseBO
    {
        public BOType Instance { get; protected set; }

        public override int Id => 0;

        public override Guid Guid => Guid.Empty;

        public override string Name => string.Empty;

        protected BaseBO(BOType instance)
        {
            Instance = instance;
        }
    }

    public abstract class BaseBO
    {
        public abstract int Id { get; }
        public abstract Guid Guid { get; }
        public abstract string Name { get; }
    }
}
