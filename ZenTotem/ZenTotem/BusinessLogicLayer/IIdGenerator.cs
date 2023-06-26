namespace ZenTotem.BusinessLogicLayer
{
    public interface IIdGenerator<T>
    {
        public T GetId();
    }
}
