import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketMainViewComponent } from './basket-main-view/basket-main-view.component';
import { HttpModule, JsonpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { SceneService } from 'app/services/scene.service';
import { ReservationService } from 'app/services/reservation.service';
import { TicketOptionsDialogComponent } from 'app/seating-plan/components/ticket-options-dialog/ticket-options-dialog.component';
import { FormsModule } from '@angular/forms';
import { ProductListComponent } from './product-list/product-list.component';
import { DeliveryPadComponent } from './delivery-pad/delivery-pad.component';
import { PaymentPadComponent } from './payment-pad/payment-pad.component';
import { SummaryComponent } from './summary/summary.component';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    HttpModule,
    JsonpModule,
    RouterModule
  ],
  exports: [
    BasketMainViewComponent
  ],
  declarations: [BasketMainViewComponent, ProductListComponent, DeliveryPadComponent, PaymentPadComponent, SummaryComponent]
})
export class BasketModule { }
