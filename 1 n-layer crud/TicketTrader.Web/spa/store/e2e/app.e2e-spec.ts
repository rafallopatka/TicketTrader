import { TicketTraderPage } from './app.po';

describe('ticket-trader App', () => {
  let page: TicketTraderPage;

  beforeEach(() => {
    page = new TicketTraderPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
