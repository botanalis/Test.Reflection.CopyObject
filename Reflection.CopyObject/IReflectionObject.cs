namespace Reflection.CopyObject
{
    public interface IReflectionObject
    {
        /// <summary>
        /// 複製 Object
        /// </summary>
        /// <param name="srcObj">來源 Object</param>
        /// <param name="destObj">建立 Object</param>
        /// <typeparam name="T">來源 Object Class</typeparam>
        /// <typeparam name="R">建立 Object Class</typeparam>
        /// <returns></returns>
        public R CopyObject<T, R>(T srcObj, R destObj);
    }
}