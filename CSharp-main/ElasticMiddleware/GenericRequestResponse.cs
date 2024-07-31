using Newtonsoft.Json;

namespace br.com.sharklab.elasticsearch
{
    public class GenericRequestResponse<T>
    {
        public T Request { get; set; }
        public T Response { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}