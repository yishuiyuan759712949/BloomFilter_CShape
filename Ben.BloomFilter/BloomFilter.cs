namespace Ben.HQ
{
    using System;
    using Utils;

    public class MyBloomFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public MyBloomFilter()
        {
            bitset = new BoolArray(Size,false);
            for (int i = 0; i < seeds.Length; i++)
            {
                functions[i] = new HashFunction(Size, seeds[i]);
            }
            
        }

        /**
         * 一个长度为10 亿的比特位
         */
        public static int Size { get; set; } = 256 << 22;

        /**
         * 为了降低错误率，使用加法hash算法，所以定义一个8个元素的质数数组
         */
        private static int[] seeds = { 3, 5, 7, 11, 13, 31, 37, 61 };

        /**
         * 相当于构建 8 个不同的hash算法
         */
        private static HashFunction[] functions = new HashFunction[seeds.Length];

        private static BoolArray bitset;

        /**
         * 初始化布隆过滤器的 bitmap
         */
        public static BoolArray Bitset
        {
            get
            {
                return bitset;
            }
            set{
                bitset = value;
            }
        }

        /**
         * 添加数据
         *
         * @param value 需要加入的值
         */
        public static void Add(String value)
        {
            if (value != null)
            {
                foreach (HashFunction f in functions)
                {
                    bitset.Set(f.hash(value), true);
                    //计算 hash 值并修改 bitmap 中相应位置为 true
                }
            }
        }

        /**
         * 判断相应元素是否存在
         * @param value 需要判断的元素
         * @return 结果
         */
        public static bool Contains(String value)
        {
            if (value == null)
            {
                return false;
            }
            bool ret = true;
            foreach (HashFunction f in functions)
            {
                ret = bitset.Get(f.hash(value));
                //一个 hash 函数返回 false 则跳出循环
                if (!ret)
                {
                    break;
                }
            }
            return ret;
        }


    }

    class HashFunction
    {

        private int size;
        private int seed;

        public HashFunction(int size, int seed)
        {
            this.size = size;
            this.seed = seed;
        }

        public int hash(String value)
        {
            int result = 0;
            int len = value.Length;
            for (int i = 0; i < len; i++)
            {
                result = seed * result + value[i];
            }
            int r = (size - 1) & result;
            return (size - 1) & result;
        }
    }

}

