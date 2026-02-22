
- Objective:
	- Analyse real-world maintenance scenarios in Azure
	- Determine appropriate patching and performance tuning strategies
	- Maintain both security and application responsiveness
- Focus:
	- Minimise downtime
	- Align maintenance with usage patterns
	- Use Azure platform capabilities effectively

---
## Example 1: Maintenance Strategy for an Ordering System

- Scenario Overview:
	- Online ordering system hosted on Azure App Services
	- Uses Azure SQL database
	- Moderate daily traffic
	- Peak usage between 2–5 p.m.
	- Downtime during peak hours risks revenue and customer trust

### Patching Strategy

- Selected Strategy:
	- Automated patching with scheduled update windows
- Implementation:
	- Configure patch window during off-peak hours (e.g., overnight or early morning)
	- Allow Azure-managed service updates to apply automatically
	- Monitor patch compliance across instances
- Rationale:
	- Avoids disruption during high-traffic periods
	- Ensures consistent security updates
	- Reduces manual intervention and operational risk
- Impact Minimisation:
	- No interruptions during 2–5 p.m. peak window
	- Maintains uptime while staying protected against vulnerabilities

### Performance Tuning Strategy

- Identified Issue:
	- Slower response times during 2–5 p.m. traffic spike
- Selected Strategy:
	- Scaling out using autoscale rules
- Implementation:
	- Configure scaling rules based on CPU or memory thresholds
	- Automatically add instances during peak hours
	- Scale back down during off-peak times
- Rationale:
	- Matches capacity to predictable demand spikes
	- Avoids over-provisioning outside peak window
- Outcome:
	- Improved responsiveness
	- Maintained availability during revenue-critical hours

---
## Example 2: Maintenance Strategy for a Scheduling App

- Scenario Overview:
	- Healthcare scheduling app on Azure App Services
	- Uses Azure SQL database
	- Active daily from 8 a.m. to 6 p.m.
	- Noticeable slowdown in late afternoon
	- Downtime impacts patient care and staff workflows

### Patching Strategy

- Selected Strategy:
	- Automated patching with off-hours scheduling
- Implementation:
	- Configure patch window between 2 a.m. – 4 a.m.
	- Apply updates automatically across all instances
	- Monitor patch compliance regularly
- Rationale:
	- Prevents disruption during 8 a.m. – 6 p.m. operating hours
	- Ensures healthcare system remains secure
	- Scales efficiently as infrastructure grows
- Impact Minimisation:
	- No service interruptions during clinical operations
	- Continuous protection against security threats

### Performance Tuning Strategy

- Likely Issue:
	- Increased concurrent usage by late afternoon
	- Resource saturation (CPU or memory) on App Service instances
- Selected Strategy:
	- Scaling out during peak usage window
- Implementation:
	- Configure autoscale rules for 3 p.m. – 6 p.m.
	- Add instances when CPU or memory exceeds defined thresholds
	- Scale down automatically after peak hours
- Rationale:
	- Directly addresses user-reported slowdowns
	- No application code changes required
	- Cost-efficient through dynamic scaling
- Preventive Outcome:
	- Sustained responsiveness during critical hours
	- Reduced risk of delays affecting patient scheduling
	- Flexible adjustment as usage patterns evolve

---
## Key Takeaways

- Maintenance decisions should:
	- Align with business-critical usage windows
	- Balance security and availability
	- Use Azure automation features strategically
- Effective combination:
	- Off-hours automated patching
	- Rule-based autoscaling for predictable load increases
- Result:
	- Secure, performant, and reliable Azure-hosted applications