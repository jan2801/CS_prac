using System;

namespace Lab1
{
    class DataChangedEventArgs
    {

        
        public ChangeInfo info_ch { get; set; }

        public string st { get; set; }

        public DataChangedEventArgs(ChangeInfo c, string s)
        {
            info_ch = c;
            st = s;
        }

        public override string ToString()
        {

            return $"this is type of change: {info_ch} {st}";

        }


    }

}