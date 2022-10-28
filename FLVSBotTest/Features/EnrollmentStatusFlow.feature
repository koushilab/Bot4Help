Feature: EnrollmentStatusFlow

@ui @web
Scenario: Verify the trigger words for enrollment question
Given I have initilaized FLVS Bot
When I interact with bot using the "Hello" message
Then I should see the response "Hi! I’m a virtual agent. I can help with account questions, orders, store information, and more." from bot
And I should see the response "If you'd like to speak to a human agent, let me know at any time." from bot
And I should see the response "So, what can I help you with today?" from bot
When I interact with bot using the "Enrollment" message
And I should see the response "What’s your enrollment question? You can also select from our top enrollment FAQs." from bot
Then I see the following EnrollmentOptions
| EnrollmentOptions |
| Placement update? |
| How to enroll?    |
| Reinstatement?    |
| Withdrawal?       |

Scenario: Verify the dialog flow for placement update
Given I interact with bot using the "Placement update" message
Then I should be seeing the adaptive cards for "placement update" with images
And I should see the response "If you have been waiting more than 14 days to be placed in your course, you may transfer to a live agent now for assistance or submit a help ticket (avg response time)." from bot
Then I see the following FollowUp questions for "Placement update"
| Questions            |
| Chat with live agent |
| Submit ticket        |

Scenario: Verify the dialog flow for reinstatement with postive flow
Given I interact with bot using the "reinstatement" message
Then I should be seeing the adaptive cards for "reinstatement" with images
And I should see the response "Did this help resolve your issue?" from bot
Then I see the following FollowUp questions for "reinstatement"
| Questions |
| yes       |
| No        |
When I interact with bot using the "Yes" message
Then I should see the "end of conversation" flow

Scenario: Verify the dialog flow for reinstatement with transfer to live agent flow
Given I interact with bot using the "reinstatement" message
Then I should be seeing the adaptive cards for "reinstatement" with images
And I should see the response "Did this help resolve your issue?" from bot
Then I see the following FollowUp questions for "reinstatement"
| Questions |
| yes       |
| No        |
When I interact with bot using the "No" message
Then I should see the "Transfer to live agent" flow

Scenario: Verify the dialog flow for withdrawal with postive flow
Given I interact with bot using the "widthdrawal" message
And I should see the response "If you are currently Active (A) or Classroom Assigned (CA), you must contact your instructor via phone or email to submit a request to be withdrawn." from bot
Then I should be seeing the adaptive cards for "widthdrawal" with images
And I should see the response "Did this help resolve your issue?" from bot
Then I see the following FollowUp questions for "reinstatement"
| Questions |
| yes       |
| No        |
When I interact with bot using the "Yes" message
Then I should see the "end of conversation" flow

Scenario: Verify the dialog flow for withdrawal with transfer to live agent flow
Given I interact with bot using the "withdrawal" message
And I should see the response "If you are currently Active (A) or Classroom Assigned (CA), you must contact your instructor via phone or email to submit a request to be withdrawn." from bot
Then I should be seeing the adaptive cards for "widthdrawal" with images
And I should see the response "Did this help resolve your issue?" from bot
Then I see the following FollowUp questions for "reinstatement"
| Questions |
| yes       |
| No        |
When I interact with bot using the "No" message
Then I should see the "Transfer to live agent" flow


