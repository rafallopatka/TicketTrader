import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule, JsonpModule } from '@angular/http';

import { PlanVisualizationComponent } from './components/plan-visualization/plan-visualization.component';
import { ReservationsPadComponent } from './components/reservations-pad/reservations-pad.component';
import { TicketOptionsPadComponent } from './components/ticket-options-pad/ticket-options-pad.component';
import { SeatingPlanMainViewComponent } from './components/seating-plan-main-view/seating-plan-main-view.component';
import { SvgMapDirective } from './directives//svg-map.directive';
import { RouterModule } from '@angular/router';
import { SceneService } from 'app/services/scene.service';
import { ReservationService } from 'app/services/reservation.service';
import { TicketOptionsDialogComponent } from 'app/seating-plan/components/ticket-options-dialog/ticket-options-dialog.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    HttpModule,
    JsonpModule,
    RouterModule
  ],
  exports: [
    SeatingPlanMainViewComponent, SvgMapDirective
  ],
  declarations: [PlanVisualizationComponent,
    ReservationsPadComponent,
    TicketOptionsPadComponent,
    SeatingPlanMainViewComponent,
    SvgMapDirective,
    TicketOptionsDialogComponent],
  providers: [SceneService, ReservationService],
})
export class SeatingPlanModule { }
