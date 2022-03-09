using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "ReadyForShipping")]
        ReadyForShipping,

        [EnumMember(Value = "Shipped")]
        Shipped,

        [EnumMember(Value = "Completed")]
        Completed,

        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}
