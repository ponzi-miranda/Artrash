using WebImportaciones.DAOs;
using WebImportaciones.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Service
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service : System.Web.Services.WebService
    {
        public Service()
        {
            DAOHelper.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        }

        [WebMethod]
        public void DeleteInscription(int id)
        {
            DAOBase<InscriptionsDTO> dao = new DAOBase<InscriptionsDTO>();
            dao.Delete(id);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataTable GetDataTable(string query)
        {
            return DAOHelper.GetDataTable(query);
        }

        [WebMethod]
        public List<UsersDTO> GetUsers(string filtro)
        {
            DAOBase<UsersDTO> dao = new DAOBase<UsersDTO>();
            var users = dao.ReadAll($"users LIKE '%{filtro}%'");
            users.ForEach(x => x.password = "********");
            return users;
        }
        
        [WebMethod]
        public UsersDTO GetBrandByID(int id)
        {
            DAOBase<UsersDTO> dao = new DAOBase<UsersDTO>();
            return dao.Read(id);
        }

        [WebMethod]
        public List<EventsDTO> GetEvents()
        {
            DAOBase<EventsDTO> dao = new DAOBase<EventsDTO>();
            return dao.ReadAll();
        }

        [WebMethod]
        public EventsDTO GetEventByID(int eventID)
        {
            DAOBase<EventsDTO> dao = new DAOBase<EventsDTO>();
            return dao.Read(eventID);
        }

        [WebMethod]
        public RolesDTO GetRolByID(int rolID)
        {
            DAOBase<RolesDTO> dao = new DAOBase<RolesDTO>();
            return dao.Read(rolID);
        }

        [WebMethod]
        public List<RolesDTO> GetRoles()
        {
            DAOBase<RolesDTO> dao = new DAOBase<RolesDTO>();
            return dao.ReadAll();
        }
        
        [WebMethod]
        public List<Product_typeDTO> GetProductTypes()
        {
            DAOBase<Product_typeDTO> dao = new DAOBase<Product_typeDTO>();
            return dao.ReadAll();
        }

        [WebMethod]
        public List<Sales_detailsDTO> GetSalesDetails()
        {
            DAOBase<Sales_detailsDTO> dao = new DAOBase<Sales_detailsDTO>();
            return dao.ReadAll();
        }

        [WebMethod]
        public List<InscriptionsDTO> GetInscriptions()
        {
            DAOBase<InscriptionsDTO> dao = new DAOBase<InscriptionsDTO>();
            return dao.ReadAll();
        }
        
        [WebMethod]
        public List<Sales_detailsDTO> GetSalesDetailsByBrandID(int brand_id)
        {
            DAOBase<Sales_detailsDTO> dao = new DAOBase<Sales_detailsDTO>();
            return dao.ReadAll($"brand_id='{brand_id}'");
        }
        
        [WebMethod]
        public List<Sales_detailsDTO> GetSalesDetailsBySaleID(int sale_id)
        {
            DAOBase<Sales_detailsDTO> dao = new DAOBase<Sales_detailsDTO>();
            return dao.ReadAll($"sale_id='{sale_id}'");
        }

        [WebMethod]
        public List<SalesDTO> GetSales()
        {
            DAOBase<SalesDTO> dao = new DAOBase<SalesDTO>();
            return dao.ReadAll();
        }
        
        [WebMethod]
        public List<SalesDTO> GetSalesByEventID(int event_id)
        {
            DAOBase<SalesDTO> dao = new DAOBase<SalesDTO>();
            return dao.ReadAll($"event_id='{event_id}'");
        }

        [WebMethod]
        public SalesDTO GetSaleByID(int id)
        {
            DAOBase<SalesDTO> dao = new DAOBase<SalesDTO>();
            return dao.Read(id);
        }

        [WebMethod]
        public List<ProductsDTO> GetProducts()
        {
            DAOBase<ProductsDTO> dao = new DAOBase<ProductsDTO>();
            return dao.ReadAll();
        }
        
        [WebMethod]
        public List<ProductsDTO> GetProductsByBrandID(int brand_id)
        {
            DAOBase<ProductsDTO> dao = new DAOBase<ProductsDTO>();
            return dao.ReadAll($"brand_id='{brand_id}'");
        }

        [WebMethod]
        public Product_typeDTO GetProductTypeByID(int id)
        {
            DAOBase<Product_typeDTO> dao = new DAOBase<Product_typeDTO>();
            return dao.Read(id);
        }
        
        [WebMethod]
        public List<StockDTO> GetStockByProductID(int product_id)
        {
            DAOBase<StockDTO> dao = new DAOBase<StockDTO>();
            return dao.ReadAll($"product_id='{product_id}'");
        }

        [WebMethod]
        public List<StockDTO> GetStockByEventID(int event_id)
        {
            DAOBase<StockDTO> dao = new DAOBase<StockDTO>();
            return dao.ReadAll($"event_id='{event_id}'");
        }

        [WebMethod]
        public List<StockDTO> GetStockByBrandID(int brand_id)
        {
            DAOBase<StockDTO> dao = new DAOBase<StockDTO>();
            return dao.ReadAll($"brand_id='{brand_id}'");
        }
        
        [WebMethod]
        public List<UsersDTO> GetBrands()
        {
            DAOBase<UsersDTO> dao = new DAOBase<UsersDTO>();
            return dao.ReadAll($"roleId=2");
        }
        
        [WebMethod]
        public List<InscriptionsDTO> GetInscriptionsByEventID(int event_id)
        {
            DAOBase<InscriptionsDTO> dao = new DAOBase<InscriptionsDTO>();
            return dao.ReadAll($"event_id={event_id}");
        }

        [WebMethod]
        public List<InscriptionsDTO> GetInscriptionsByBrandID(int brand_id)
        {
            DAOBase<InscriptionsDTO> dao = new DAOBase<InscriptionsDTO>();
            return dao.ReadAll($"brand_id={brand_id}");
        }
        
        [WebMethod]
        public ProductsDTO GetProductByID(int product_id)
        {
            DAOBase<ProductsDTO> dao = new DAOBase<ProductsDTO>();
            return dao.Read(product_id);
        }

        [WebMethod]
        public List<UsersDTO> Login(string email, string password)
        {
            DAOBase<UsersDTO> dao = new DAOBase<UsersDTO>();
            return dao.ReadAll($"email LIKE '{email}' AND password='{password}'");
        }

        [WebMethod]
        public UsersDTO SetUser(UsersDTO user)
        {
            DAOBase<UsersDTO> dao = new DAOBase<UsersDTO>();

            if (user.Id > 0)
            {
                dao.Update(user);
                return user;
            }
            else
            {
                dao.Insert(user);
                return user;
            }
        }
        
        [WebMethod]
        public SalesDTO SetSale(SalesDTO sale)
        {
            DAOBase<SalesDTO> dao = new DAOBase<SalesDTO>();

            if (sale.Id > 0)
            {
                dao.Update(sale);
                return sale;
            }
            else
            {
                dao.Insert(sale);
                return sale;
            }
        }
        
        [WebMethod]
        public Sales_PaymentsDTO SetSalePayment(Sales_PaymentsDTO salePayment)
        {
            DAOBase<Sales_PaymentsDTO> dao = new DAOBase<Sales_PaymentsDTO>();

            if (salePayment.Id > 0)
            {
                dao.Update(salePayment);
                return salePayment;
            }
            else
            {
                dao.Insert(salePayment);
                return salePayment;
            }
        }

        [WebMethod]
        public List<PaymentsDTO> GetPayments()
        {
            DAOBase<PaymentsDTO> dao = new DAOBase<PaymentsDTO>();
            return dao.ReadAll();
        }

        [WebMethod]
        public PaymentsDTO GetPaymentByID(int payment_id)
        {
            DAOBase<PaymentsDTO> dao = new DAOBase<PaymentsDTO>();
            return dao.Read(payment_id);
        }

        [WebMethod]
        public List<Sales_PaymentsDTO> GetSalePaymentsBySaleID(int sale_id)
        {
            DAOBase<Sales_PaymentsDTO> dao = new DAOBase<Sales_PaymentsDTO>();
            return dao.ReadAll($"IdSale={sale_id}");
        }
        
        [WebMethod]
        public List<Sales_PaymentsDTO> GetSalePayments()
        {
            DAOBase<Sales_PaymentsDTO> dao = new DAOBase<Sales_PaymentsDTO>();
            return dao.ReadAll();
        }

        [WebMethod]
        public Sales_detailsDTO SetSaleDetail(Sales_detailsDTO sale_detail)
        {
            DAOBase<Sales_detailsDTO> dao = new DAOBase<Sales_detailsDTO>();

            if (sale_detail.Id > 0)
            {
                dao.Update(sale_detail);
                return sale_detail;
            }
            else
            {
                dao.Insert(sale_detail);
                return sale_detail;
            }
        }

        [WebMethod]
        public StockDTO SetStock(StockDTO stock)
        {
            DAOBase<StockDTO> dao = new DAOBase<StockDTO>();

            if (stock.Id > 0)
            {
                dao.Update(stock);
                return stock;
            }
            else
            {
                dao.Insert(stock);
                return stock;
            }
        }

        [WebMethod]
        public ProductsDTO SetProduct(ProductsDTO product)
        {
            DAOBase<ProductsDTO> dao = new DAOBase<ProductsDTO>();

            if (product.Id > 0)
            {
                dao.Update(product);
                return product;
            }
            else
            {
                dao.Insert(product);
                return product;
            }
        }  
        
        [WebMethod]
        public InscriptionsDTO SetInscription(InscriptionsDTO inscription)
        {
            DAOBase<InscriptionsDTO> dao = new DAOBase<InscriptionsDTO>();

            if (inscription.Id > 0)
            {
                dao.Update(inscription);
                return inscription;
            }
            else
            {
                dao.Insert(inscription);
                return inscription;
            }
        }

        [WebMethod]
        public void DeleteProductsPromoByIdPromo(int idPromo)
        {
            DAOBase<Promotions_ProductsDTO> dao = new DAOBase<Promotions_ProductsDTO>();

            var productsPromo = GetProductsPromoByIdPromo(idPromo);

            if (productsPromo != null)
            {
                foreach (var item in productsPromo)
                {
                    dao.Delete(item.Id);
                }
            }
        }

        [WebMethod]
        public EventsDTO SetEvent(EventsDTO newEvent)
        {
            DAOBase<EventsDTO> dao = new DAOBase<EventsDTO>();

            if (newEvent.Id > 0)
            {
                dao.Update(newEvent);
                return newEvent;
            }
            else
            {
                dao.Insert(newEvent);
                return newEvent;
            }
        }

        [WebMethod]
        public List<SecurityDTO> GetSecurity(int idRol, string program)
        {
            DAOBase<SecurityDTO> daoPermiso = new DAOBase<SecurityDTO>();
            return daoPermiso.ReadAll($"IdRol={idRol} AND Program='{program}'");
        }

        [WebMethod]
        public List<SecurityDTO> SetPermisos(int idPerfil, string nombrePrograma, List<SecurityDTO> permisos)
        {
            DAOBase<SecurityDTO> daoPermiso = new DAOBase<SecurityDTO>();
            daoPermiso.DeleteAll($"IdRol={idPerfil} AND Program='{nombrePrograma}'");

            foreach (var permiso in permisos)
            {
                permiso.IdRol = idPerfil;
                permiso.Program = nombrePrograma;
                daoPermiso.Insert(permiso);
            }

            return GetSecurity(idPerfil, nombrePrograma);
        }

        [WebMethod]
        public List<SecurityDTO> GetSecurityByRol(int rol, string nombrePrograma)
        {
            DAOBase<SecurityDTO> daoPermiso = new DAOBase<SecurityDTO>();
            return daoPermiso.ReadAll($"IdRol={rol} AND Program='{nombrePrograma}'");
        }

        [WebMethod]
        public byte[] EjecutarReporte1(string archivoReporte, List<KeyValuePair<string, object>> parametros)
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //cr.Load(Server.MapPath($"~/Reports/{archivoReporte}"), CrystalDecisions.Shared.OpenReportMethod.OpenReportByTempCopy);
            cr.SetDatabaseLogon("db_9cf8b6_arttrash_admin", "q1w2e3r4");

            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                cr.SetParameterValue(parametro.Key, parametro.Value);
            }

            //Recordar qque el RPT debe estar apuntando a la DB correcta y con sus credenciales guardadas.
            var bytes = GetStreamAsByteArray(cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));

            cr.Close();
            cr.Dispose();

            return bytes;
        }


        [WebMethod]
        public byte[] CR_Venta(int idVenta)
        {
            return EjecutarReporte1("IMG_OrdenServicio_v01.rpt",
            new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("IdSale", idVenta)
            });
        }

        [WebMethod]
        public byte[] CR_Stock(int idMarca, int idEvento)
        {
            return EjecutarReporte1("IMG_Oportunidad - copia.rpt",
            new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("IdEvento", idEvento),
                new KeyValuePair<string, object>("IdMarca", idMarca)
            });
        }

        [WebMethod]
        public byte[] CR_Sales(int idMarca, int idEvento)
        {
            return EjecutarReporte1("IMG_OrdenServicio - copia.rpt",
            new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("IdEvento", idEvento),
                new KeyValuePair<string, object>("IdMarca", idMarca)
            });
        }

        private static byte[] GetStreamAsByteArray(System.IO.Stream stream)
        {
            int streamLength = Convert.ToInt32(stream.Length);
            byte[] fileData = new byte[streamLength + 1];

            stream.Read(fileData, 0, streamLength);
            stream.Close();

            return fileData;
        }

        [WebMethod]
        public List<Promotions_ProductsDTO> GetPromotionsByIdProduct(int idProduct)
        {
            DAOBase<Promotions_ProductsDTO> dao = new DAOBase<Promotions_ProductsDTO>();
            return dao.ReadAll($"idProduct='{idProduct}'");
        }

        [WebMethod]
        public List<PromotionsDTO> GetPromotionsByIdBrand(int idBrand)
        {
            DAOBase<PromotionsDTO> dao = new DAOBase<PromotionsDTO>();
            return dao.ReadAll($"idBrand='{idBrand}'");
        }

        [WebMethod]
        public List<PromotionsDTO> GetPromotions()
        {
            DAOBase<PromotionsDTO> dao = new DAOBase<PromotionsDTO>();
            return dao.ReadAll();
        }

        [WebMethod]
        public List<PromotionsDTO> FindAllPromociones(string filter)
        {
            filter = filter.Replace("'", string.Empty);
            DAOBase<PromotionsDTO> dao = new DAOBase<PromotionsDTO>();
            return dao.ReadAll($"descripcion LIKE '%{filter}%' AND state Like 'Activo'");
        }       

        [WebMethod]
        public PromotionsDTO GetPromotionById(int id)
        {
            DAOBase<PromotionsDTO> dao = new DAOBase<PromotionsDTO>();
            return dao.Read(id);
        }

        [WebMethod]
        public PromotionsDTO SetPromotion(PromotionsDTO producto)
        {
            DAOBase<PromotionsDTO> dao = new DAOBase<PromotionsDTO>();

            if (producto.Id > 0)
            {
                dao.Update(producto);
                return producto;
            }
            {
                dao.Insert(producto);
                return producto;
            }
        }
        [WebMethod]
        public Promotions_ProductsDTO SetProductsPromotions(Promotions_ProductsDTO producto)
        {
            DAOBase<Promotions_ProductsDTO> dao = new DAOBase<Promotions_ProductsDTO>();

            if (producto.Id > 0)
            {
                dao.Update(producto);
                return producto;
            }
            {
                dao.Insert(producto);
                return producto;
            }
        }

        [WebMethod]
        public List<Promotions_ProductsDTO> GetProductsPromoByIdPromo(int idPromo)
        {
            DAOBase<Promotions_ProductsDTO> dao = new DAOBase<Promotions_ProductsDTO>();
            return dao.ReadAll($"idPromotion='{idPromo}'");
        }
    }
}
