﻿using DonaLaura.Domain.Common;
using DonaLaura.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonaLaura.Domain.Features.Orders
{
    public  class Order : Entity
    {
        public virtual string Client { get; set; }

        public virtual Product product { get; set; }

        public virtual int Amount { get; set; }

        public virtual double Profit { get; set; }


        public override void Validate()
        {
            if (String.IsNullOrEmpty(Client))
                throw new OrderClientNullOrEmptyException();
            if (Client.Length <= 3)
                throw new OrderClientNameShortException();
            if (product == null)
                throw new OrderProductNullException();
        }
    }
}
