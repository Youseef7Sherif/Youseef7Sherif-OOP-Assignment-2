WardBoard

- Responsibilities:
  - Manages bed and patient assignments.
  - Calculates patient acuity scores.
  - Records pager alerts based on acuity thresholds.
  - Builds nurse handoff notes.
  - Exports ward census data as CSV.

- Why combining is a problem:
  These responsibilities can change independently. Clinical scoring,
  pager rules, note formatting, and CSV export should not all require
  changes to the same class.

CheckoutBasket

- Responsibilities:
  - Manages items in the basket, including their SKUs, prices, and quantities.
  - Calculates the basket subtotal, discounts, and final total.
  - Parses coupon text and applies discount rules.
  - Handles gift-wrap selection and generates gift message cards.
  - Generates payment authorization codes.

- Why combining is a problem:
  These responsibilities have different reasons to change. Basket and pricing rules, coupon and marketing rules, gift-message formatting, and payment authorization can all change independently, so combining them in one class makes unrelated changes affect the same class.

SupportTicket

- Responsibilities:
  - Stores and manages support ticket data, including the subject, body, and customer messages.
  - Determines ticket priority based on keywords in the ticket text.
  - Calculates the SLA deadline based on the ticket priority.
  - Checks whether the SLA deadline has been breached.
  - Generates customer-facing public replies.
  - Generates internal escalation messages.

- Why combining is a problem:
  Ticket data management, priority rules, SLA deadline calculation, breach checking, and message formats have different reasons to change. Changes in one area would require modifying the same class even when the other responsibilities remain unchanged.

LoanDesk

- Responsibilities:
  - Stores loan application data such as the requested amount, credit score, employment history, and collateral status.
  - Calculates the loan risk score.
  - Determines whether the loan application is eligible.
  - Determines the required documents for the application.
  - Generates the loan decision letter.
  - Exports loan application data as a CSV row for the underwriter.

- Why combining is a problem:
  These responsibilities have different reasons to change. Risk and eligibility rules, document requirements, decision-letter wording, and CSV export formatting can change independently, so changes in one area would require modifying the same class.

CourseEnrollmentDesk

- Responsibilities:
  - Manages course enrollment, including seat capacity, student registration, waitlist positions, and waitlist promotion.
  - Generates welcome packet content for enrolled or waitlisted students.
  - Generates tuition invoice lines, including tuition and VAT calculations.

- Why combining is a problem:
  Enrollment and waitlist rules, welcome-packet content, and tuition/invoice rules have different reasons to change. Changes in one area would require modifying the same class even when the other responsibilities remain unchanged.

KitchenTicket

- Responsibilities:
  - Manages kitchen order items, including their ingredients and preparation times.
  - Detects allergens from the ingredients of the order items.
  - Estimates the order preparation time based on preparation times, available kitchen stations, and allergy protocol delays.
  - Renders the order as a thermal printer ticket, including its layout and formatting.
  - Determines the appropriate kitchen expo lane based on allergens and estimated preparation time.

- Why combining is a problem:
  These responsibilities have different reasons to change. Allergen detection rules, kitchen timing and operational rules, thermal printer formatting, and expo lane rules can change independently, so changes in one area would require modifying the same class.

GradeBook

- Responsibilities:
  - Stores and records students' scores.
  - Calculates students' average scores.
  - Determines letter grades based on academic grading rules.
  - Determines whether a student meets the Honor Roll requirements.
  - Generates plain-text transcripts for students.
  - Exports students' grading information as CSV.

- Why combining is a problem:
  These responsibilities have different reasons to change. Score management, grading policies, Honor Roll rules, transcript formatting, and CSV export formatting can change independently, so changes in one area would require modifying the same class.

SubscriptionBilling

- Responsibilities:
  - Stores subscription and customer billing information, including the customer ID, monthly price, billing period, and failed payment count.
  - Calculates prorated subscription charges based on the active period.
  - Generates sequential invoice numbers.
  - Tracks failed payments.
  - Generates dunning email content based on the customer's failed payment history.
  - Generates accounting ledger journal lines for subscription billing.

- Why combining is a problem:
  These responsibilities have different reasons to change. Proration and billing rules, invoice numbering, failed-payment tracking, collection email content, and accounting export formatting can change independently, so changes in one area would require modifying the same class.

AppointmentDesk

- Responsibilities:
  - Manages appointment scheduling information, including business hours, slot duration, and booked appointments.
  - Determines whether appointment times are within business hours.
  - Finds available appointment slots and books appointments.
  - Generates ICS calendar event data for appointments.
  - Generates SMS reminder messages for appointments.

- Why combining is a problem:
  These responsibilities have different reasons to change. Appointment scheduling and business-hour rules, calendar interoperability formatting, and SMS message content can change independently, so changes in one area would require modifying the same class.

WarehousePickList

- Responsibilities:
  - Stores warehouse picking requirements, including SKU, location, required quantity, and available stock.
  - Allocates available stock against the required quantities.
  - Determines the walking order for picking items based on warehouse locations.
  - Generates picker instructions and reports shortages.
  - Generates XML batches for integration with the Warehouse Management System (WMS).

- Why combining is a problem:
  These responsibilities have different reasons to change. Stock allocation rules, warehouse path ordering, picker instruction wording, and WMS integration formatting can change independently, so changes in one area would require modifying the same class.
