using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TicketTrader.Dal;
using TicketTrader.Model;

namespace TicketTrader.Dal.Migrations
{
    [DbContext(typeof(DalContext))]
    [Migration("20170808212310_ticket-reservations")]
    partial class ticketreservations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("TicketTrader.Model.AdditionalProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OrderId");

                    b.ToTable("AdditionalProducts");
                });

            modelBuilder.Entity("TicketTrader.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Client");
                });

            modelBuilder.Entity("TicketTrader.Model.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<int?>("ServiceFeeId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceFeeId");

                    b.ToTable("Deliveries");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Delivery");
                });

            modelBuilder.Entity("TicketTrader.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("DescriptionId");

                    b.Property<TimeSpan>("Duration");

                    b.Property<int?>("RootId");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionId");

                    b.HasIndex("RootId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TicketTrader.Model.EventCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EventCategories");
                });

            modelBuilder.Entity("TicketTrader.Model.EventDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Authors");

                    b.Property<string>("Cast");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("EventDescriptions");
                });

            modelBuilder.Entity("TicketTrader.Model.EventDescriptionCategories", b =>
                {
                    b.Property<int>("DescriptionId");

                    b.Property<int>("CategoryId");

                    b.HasKey("DescriptionId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("EventDescriptionCategories");
                });

            modelBuilder.Entity("TicketTrader.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<int?>("DeliveryId");

                    b.Property<TimeSpan>("ExpirationTimeout");

                    b.Property<int?>("PaymentId");

                    b.Property<int?>("SalesDocumentId");

                    b.Property<int>("State");

                    b.Property<DateTime>("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("SalesDocumentId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TicketTrader.Model.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<int?>("ServiceFeeId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceFeeId");

                    b.ToTable("Payments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Payment");
                });

            modelBuilder.Entity("TicketTrader.Model.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("GrossAmount");

                    b.Property<decimal>("NetAmount");

                    b.Property<decimal>("VatRate");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("TicketTrader.Model.PriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("TicketTrader.Model.PriceOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("PriceId");

                    b.Property<int>("PriceZoneId");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.HasIndex("PriceZoneId");

                    b.ToTable("PriceOptions");
                });

            modelBuilder.Entity("TicketTrader.Model.PriceZone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("PriceListId");

                    b.Property<int>("ScenePriceZoneId");

                    b.HasKey("Id");

                    b.HasIndex("PriceListId");

                    b.ToTable("PriceZones");
                });

            modelBuilder.Entity("TicketTrader.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<bool>("Discarded");

                    b.Property<int>("EventId");

                    b.Property<int>("OrderId");

                    b.Property<int>("SeatId");

                    b.Property<int?>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EventId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SeatId");

                    b.HasIndex("TicketId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TicketTrader.Model.SalesDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SalesDocuments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SalesDocument");
                });

            modelBuilder.Entity("TicketTrader.Model.Scene", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<int>("EventId");

                    b.Property<int?>("RootId");

                    b.Property<string>("UniqueName");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.HasIndex("RootId");

                    b.ToTable("Scenes");
                });

            modelBuilder.Entity("TicketTrader.Model.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("EventId");

                    b.Property<int?>("PriceZoneId");

                    b.Property<int?>("SceneId");

                    b.Property<int>("SceneSeatId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PriceZoneId");

                    b.HasIndex("SceneId");

                    b.ToTable("Seats");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Seat");
                });

            modelBuilder.Entity("TicketTrader.Model.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("SceneSectorId");

                    b.HasKey("Id");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("TicketTrader.Model.TicketProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<int>("EventId");

                    b.Property<int>("OrderId");

                    b.Property<int?>("PriceOptionId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EventId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PriceOptionId");

                    b.ToTable("TicketProducts");
                });

            modelBuilder.Entity("TicketTrader.Model.AnonymousClient", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Client");


                    b.ToTable("AnonymousClient");

                    b.HasDiscriminator().HasValue("AnonymousClient");
                });

            modelBuilder.Entity("TicketTrader.Model.BusinessClient", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Client");

                    b.Property<string>("Address");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Nip");

                    b.Property<int>("UserId");

                    b.ToTable("BusinessClient");

                    b.HasDiscriminator().HasValue("BusinessClient");
                });

            modelBuilder.Entity("TicketTrader.Model.IndividualClient", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Client");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("FistName");

                    b.Property<string>("IdentityUserId");

                    b.Property<string>("LastName");

                    b.Property<string>("PostalCode");

                    b.ToTable("IndividualClient");

                    b.HasDiscriminator().HasValue("IndividualClient");
                });

            modelBuilder.Entity("TicketTrader.Model.DirectDelivery", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Delivery");


                    b.ToTable("DirectDelivery");

                    b.HasDiscriminator().HasValue("DirectDelivery");
                });

            modelBuilder.Entity("TicketTrader.Model.DispatchDelivery", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Delivery");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("PostalCode");

                    b.ToTable("DispatchDelivery");

                    b.HasDiscriminator().HasValue("DispatchDelivery");
                });

            modelBuilder.Entity("TicketTrader.Model.OnlineDelivery", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Delivery");

                    b.Property<string>("Email");

                    b.ToTable("OnlineDelivery");

                    b.HasDiscriminator().HasValue("OnlineDelivery");
                });

            modelBuilder.Entity("TicketTrader.Model.CardPayment", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Payment");


                    b.ToTable("CardPayment");

                    b.HasDiscriminator().HasValue("CardPayment");
                });

            modelBuilder.Entity("TicketTrader.Model.CashPayment", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Payment");


                    b.ToTable("CashPayment");

                    b.HasDiscriminator().HasValue("CashPayment");
                });

            modelBuilder.Entity("TicketTrader.Model.OnlinePayment", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Payment");


                    b.ToTable("OnlinePayment");

                    b.HasDiscriminator().HasValue("OnlinePayment");
                });

            modelBuilder.Entity("TicketTrader.Model.Invoice", b =>
                {
                    b.HasBaseType("TicketTrader.Model.SalesDocument");


                    b.ToTable("Invoice");

                    b.HasDiscriminator().HasValue("Invoice");
                });

            modelBuilder.Entity("TicketTrader.Model.Receipt", b =>
                {
                    b.HasBaseType("TicketTrader.Model.SalesDocument");


                    b.ToTable("Receipt");

                    b.HasDiscriminator().HasValue("Receipt");
                });

            modelBuilder.Entity("TicketTrader.Model.NumberedSeat", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Seat");

                    b.Property<string>("Number");

                    b.Property<int?>("SectorId");

                    b.HasIndex("SectorId");

                    b.ToTable("NumberedSeat");

                    b.HasDiscriminator().HasValue("NumberedSeat");
                });

            modelBuilder.Entity("TicketTrader.Model.UnnumberedSeat", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Seat");

                    b.Property<string>("Name");

                    b.ToTable("UnnumberedSeat");

                    b.HasDiscriminator().HasValue("UnnumberedSeat");
                });

            modelBuilder.Entity("TicketTrader.Model.BusinessClientInvoice", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Invoice");


                    b.ToTable("BusinessClientInvoice");

                    b.HasDiscriminator().HasValue("BusinessClientInvoice");
                });

            modelBuilder.Entity("TicketTrader.Model.IndividualClientInvoice", b =>
                {
                    b.HasBaseType("TicketTrader.Model.Invoice");


                    b.ToTable("IndividualClientInvoice");

                    b.HasDiscriminator().HasValue("IndividualClientInvoice");
                });

            modelBuilder.Entity("TicketTrader.Model.AdditionalProduct", b =>
                {
                    b.HasOne("TicketTrader.Model.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Order", "Order")
                        .WithMany("AdditionalProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketTrader.Model.Delivery", b =>
                {
                    b.HasOne("TicketTrader.Model.Price", "ServiceFee")
                        .WithMany()
                        .HasForeignKey("ServiceFeeId");
                });

            modelBuilder.Entity("TicketTrader.Model.Event", b =>
                {
                    b.HasOne("TicketTrader.Model.EventDescription", "Description")
                        .WithMany("Events")
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Event", "Root")
                        .WithMany()
                        .HasForeignKey("RootId");
                });

            modelBuilder.Entity("TicketTrader.Model.EventDescriptionCategories", b =>
                {
                    b.HasOne("TicketTrader.Model.EventCategory", "Category")
                        .WithMany("EventCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.EventDescription", "Description")
                        .WithMany("EventCategories")
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketTrader.Model.Order", b =>
                {
                    b.HasOne("TicketTrader.Model.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Delivery", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryId");

                    b.HasOne("TicketTrader.Model.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("TicketTrader.Model.SalesDocument", "SalesDocument")
                        .WithMany()
                        .HasForeignKey("SalesDocumentId");
                });

            modelBuilder.Entity("TicketTrader.Model.Payment", b =>
                {
                    b.HasOne("TicketTrader.Model.Price", "ServiceFee")
                        .WithMany()
                        .HasForeignKey("ServiceFeeId");
                });

            modelBuilder.Entity("TicketTrader.Model.PriceList", b =>
                {
                    b.HasOne("TicketTrader.Model.Event", "Event")
                        .WithMany("PriceLists")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketTrader.Model.PriceOption", b =>
                {
                    b.HasOne("TicketTrader.Model.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId");

                    b.HasOne("TicketTrader.Model.PriceZone", "PriceZone")
                        .WithMany("Options")
                        .HasForeignKey("PriceZoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketTrader.Model.PriceZone", b =>
                {
                    b.HasOne("TicketTrader.Model.PriceList", "PriceList")
                        .WithMany("PriceZones")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketTrader.Model.Reservation", b =>
                {
                    b.HasOne("TicketTrader.Model.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Event", "Event")
                        .WithMany("Reservations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Order", "Order")
                        .WithMany("Reservations")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Seat", "Seat")
                        .WithMany("Reservations")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.TicketProduct", "Ticket")
                        .WithMany("Reservations")
                        .HasForeignKey("TicketId");
                });

            modelBuilder.Entity("TicketTrader.Model.Scene", b =>
                {
                    b.HasOne("TicketTrader.Model.Event", "Event")
                        .WithOne("Scene")
                        .HasForeignKey("TicketTrader.Model.Scene", "EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Scene", "Root")
                        .WithMany()
                        .HasForeignKey("RootId");
                });

            modelBuilder.Entity("TicketTrader.Model.Seat", b =>
                {
                    b.HasOne("TicketTrader.Model.Event", "Event")
                        .WithMany("Seats")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.PriceZone")
                        .WithMany("Seats")
                        .HasForeignKey("PriceZoneId");

                    b.HasOne("TicketTrader.Model.Scene")
                        .WithMany("Seats")
                        .HasForeignKey("SceneId");
                });

            modelBuilder.Entity("TicketTrader.Model.TicketProduct", b =>
                {
                    b.HasOne("TicketTrader.Model.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.Order", "Order")
                        .WithMany("Tickets")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketTrader.Model.PriceOption", "PriceOption")
                        .WithMany()
                        .HasForeignKey("PriceOptionId");
                });

            modelBuilder.Entity("TicketTrader.Model.NumberedSeat", b =>
                {
                    b.HasOne("TicketTrader.Model.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorId");
                });
        }
    }
}
