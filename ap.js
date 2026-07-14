// ==========================================
// JiraPulse AI - Core Logic & Ingest Engine
// ==========================================

// Global State
let issues = [];
let selectedIssue = null;
let syncQueue = [];
let originalIssuesSnapshot = [];

// Sample Datasets for Ingestion Hub
const PRESETS = {
  "production-down": [
    {
      key: "CORE-901",
      summary: "Database cluster node-3 replication delay exceeding 300s, transactions blocking",
      description: "We are seeing severe write locks on the primary database cluster. Replication lag on Node-3 is causing stale read states and checkout failures. Immediate intervention needed.",
      originalPriority: "High",
      component: "Database Cluster",
      status: "In Progress"
    },
    {
      key: "PAY-402",
      summary: "Stripe checkout Webhook failed with 504 Gateway Timeout",
      description: "Users completes payment but webhook fails to process. Customer accounts are not provisioned, triggering dozens of urgent support tickets.",
      originalPriority: "Medium",
      component: "Billing & Checkout",
      status: "To Do"
    },
    {
      key: "AUTH-804",
      summary: "Cannot login via Okta SSO: signature verification error",
      description: "Critical bug. All corporate SSO accounts are locked out. Standard email login is working, but enterprise clients are completely blocked from accessing the platform.",
      originalPriority: "Medium",
      component: "Authentication",
      status: "To Do"
    },
    {
      key: "API-309",
      summary: "Memory leak in user profile endpoint under load",
      description: "During load tests, memory allocation for web-dyno-4 increases linearly until the container crashes (OOM). Profiler indicates user session records are not being cleaned up from Redis cache.",
      originalPriority: "Medium",
      component: "User Profile UI",
      status: "To Do"
    },
    {
      key: "SEC-007",
      summary: "SQL injection vulnerability detected in advanced search field",
      description: "External whitehat report. Parameter 'q' in search endpoint does not sanitize input, allowing potential remote table extraction. Needs immediate patch.",
      originalPriority: "Low",
      component: "Search Engine",
      status: "To Do"
    },
    {
      key: "NOT-112",
      summary: "Transactional emails delayed by 45 minutes",
      description: "SendGrid queue is backed up. OTP and confirmation emails are taking almost an hour to deliver, making verification links expire before users receive them.",
      originalPriority: "High",
      component: "Notification Dispatcher",
      status: "In Progress"
    },
    {
      key: "PAY-405",
      summary: "Credit card form UI misaligned on Safari Mobile browser",
      description: "Input fields overlap on screen widths below 375px in mobile Safari. Layout works fine on Chrome/iOS and desktop web views.",
      originalPriority: "Low",
      component: "Billing & Checkout",
      status: "To Do"
    },
    {
      key: "UI-991",
      summary: "Infinite loading spinner displayed when updating avatar picture",
      description: "If avatar file exceeds 2MB, no error validation is shown. Spinner runs forever. User has to refresh the browser page manually.",
      originalPriority: "Medium",
      component: "User Profile UI",
      status: "In Review"
    }
  ],

  "backlog-triage": [
    {
      key: "FEAT-102",
      summary: "Support dark mode auto-toggle based on client system settings",
      description: "Enable system-level dark mode detection and auto-switch. We currently only support manual toggling in the preference drawer.",
      originalPriority: "Low",
      component: "User Profile UI",
      status: "To Do"
    },
    {
      key: "UI-821",
      summary: "Fix grammar mistakes on checkout success page",
      description: "Typo in header: 'Your order have been placed successfully'. Should read: 'Your order has been placed successfully'.",
      originalPriority: "Low",
      component: "Billing & Checkout",
      status: "To Do"
    },
    {
      key: "NOT-902",
      summary: "Add weekly analytics report digest to marketing user dashboard",
      description: "Business request: Compile dashboard summary charts and email them to admin-level stakeholders every Monday morning automatically.",
      originalPriority: "Medium",
      component: "Notification Dispatcher",
      status: "To Do"
    },
    {
      key: "DB-551",
      summary: "Optimize indices on static catalog search query tables",
      description: "Search queries on static inventory tables are taking 450ms. Adding index on compound columns (category, priority, active) should reduce this to ~40ms.",
      originalPriority: "Medium",
      component: "Search Engine",
      status: "In Progress"
    },
    {
      key: "AUTH-109",
      summary: "Add MFA setup guide link to security settings sidebar",
      description: "Add a static link pointing to setup instruction PDF for user profile multifactor enrollment workflow.",
      originalPriority: "Low",
      component: "Authentication",
      status: "Done"
    },
    {
      key: "FEAT-938",
      summary: "Create CSV download action button on list report dashboard table",
      description: "Users need to download tabular filter outputs as flat CSV spreadsheets directly from the interface.",
      originalPriority: "Medium",
      component: "Search Engine",
      status: "In Progress"
    },
    {
      key: "UI-104",
      summary: "Terms of service scroll container overlaps footer at 768px resolutions",
      description: "CSS overflow bug: The terms scroll box has fixed height that overflows header and footer grids on standard tablets.",
      originalPriority: "Medium",
      component: "User Profile UI",
      status: "In Review"
    },
    {
      key: "DB-112",
      summary: "Upgrade Redis instance to v7.2 for cluster compatibility",
      description: "Prepare and execute migration of cache nodes to latest version to leverage key eviction improvements.",
      originalPriority: "Low",
      component: "Database Cluster",
      status: "To Do"
    },
    {
      key: "NOT-228",
      summary: "Add email alert template customization options for admins",
      description: "Admins want to modify logo assets and header texts in email alerts using simple markdown forms.",
      originalPriority: "Medium",
      component: "Notification Dispatcher",
      status: "To Do"
    },
    {
      key: "PAY-111",
      summary: "Implement card brand detection animation on card input",
      description: "Nice-to-have visual animation: Slide brand icon (Visa/MC/Amex) based on numerical prefix during credit card form typing.",
      originalPriority: "Low",
      component: "Billing & Checkout",
      status: "Done"
    }
  ],

  "mixed-batch": [
    {
      key: "PAY-882",
      summary: "Checkout page blank screen due to missing payment config parameters",
      description: "Critical bug. A config roll-out missing active flag causes Stripe JS to error out. Checkout page becomes entirely blank, preventing any checkout transactions.",
      originalPriority: "Highest",
      component: "Billing & Checkout",
      status: "To Do"
    },
    {
      key: "AUTH-392",
      summary: "Brute-force login protection: IP block is not triggering after 10 failed passwords",
      description: "Security concern. Intrusion detection system does not restrict traffic from malicious agents who loop dictionary attacks against logins.",
      originalPriority: "Medium",
      component: "Authentication",
      status: "To Do"
    },
    {
      key: "DB-121",
      summary: "Database connection pool exhausts under heavy API load spikes",
      description: "During spikes, connections leak in user authentication middleware. Connection pool hits limit of 200 and API begins responding with 500 server errors.",
      originalPriority: "High",
      component: "Database Cluster",
      status: "In Progress"
    },
    {
      key: "SRCH-901",
      summary: "Elasticsearch index sync is broken for inventory changes",
      description: "New catalog inventory items do not appear in user search lists. Sync queue hangs after index changes are pushed from DB tier.",
      originalPriority: "High",
      component: "Search Engine",
      status: "In Progress"
    },
    {
      key: "UI-771",
      summary: "Profile editor fails silently if name field contains emoji characters",
      description: "Database rejects input length validation on profile write. UI spins without displaying error feedback to the customer.",
      originalPriority: "Low",
      component: "User Profile UI",
      status: "To Do"
    },
    {
      key: "NOT-002",
      summary: "SMS verification codes failing for international country codes",
      description: "Twilio API configuration rejects formatting for prefix codes outside of North America and Europe. Crucial for onboarding global clients.",
      originalPriority: "Medium",
      component: "Notification Dispatcher",
      status: "To Do"
    },
    {
      key: "UI-229",
      summary: "Change default font size from 14px to 15px in policy agreement body text",
      description: "Readability improvement request from corporate legal advisor. Simple font style adjustment.",
      originalPriority: "Low",
      component: "User Profile UI",
      status: "Done"
    },
    {
      key: "PAY-552",
      summary: "Refund confirmation button is disabled on manager portal dashboard",
      description: "Administrators cannot execute customer refunds. Button is unclickable despite correct ACL privilege configurations.",
      originalPriority: "High",
      component: "Billing & Checkout",
      status: "In Progress"
    },
    {
      key: "AUTH-210",
      summary: "Typo in SSO onboarding helper dialog documentation text",
      description: "Help document page reads: 'Please consult your LDAP administrator to configure the metadata URL redirectory.' Correct: 'metadata URL directory'.",
      originalPriority: "Low",
      component: "Authentication",
      status: "Done"
    },
    {
      key: "DB-199",
      summary: "Unused backup table rows in history DB slowing down reporting export scripts",
      description: "Cleaning up archive schemas from 2024 partition of events table is required to keep query durations under SLA parameters.",
      originalPriority: "Medium",
      component: "Database Cluster",
      status: "To Do"
    },
    {
      key: "SRCH-201",
      summary: "Autosuggest dropdown UI closes prematurely when typing quickly on desktop Chrome browser",
      description: "Focus boundary bug in autocomplete widget closes results list whenever debounce events fire.",
      originalPriority: "Low",
      component: "Search Engine",
      status: "To Do"
    },
    {
      key: "NOT-404",
      summary: "Push notifications fail to render on macOS Safari Browser standard alerts",
      description: "Web push integration payload signature issue on Safari causes alerts to be ignored by notification centers.",
      originalPriority: "Medium",
      component: "Notification Dispatcher",
      status: "In Review"
    }
  ]
};

// Heuristic Rules Dictionary for Priority & Criticality Analysis
const CRITICAL_KEYWORDS = [
  "payment failed", "stripe fail", "checkout fail", "cannot checkout", "blank screen",
  "downtime", "server crash", "memory leak", "auth fail", "security breach", "sql injection",
  "vulnerability", "leak", "db down", "database down", "cluster offline", "unable to login",
  "cannot login", "okta sso", "checkout failure", "unusable", "outage", "production down"
];

const HIGH_KEYWORDS = [
  "slow", "performance", "delay", "lag", "exhausts", "timeout", "broken link",
  "fails silently", "disabled", "cannot access", "fails to process", "malicious", "blocking"
];

const LOW_KEYWORDS = [
  "typo", "grammar", "spelling", "documentation", "color tweak", "font size", 
  "misaligned", "overlap", "nice-to-have", "weekly analytics", "avatar picture", "spinner"
];

// Heuristic Classification Engine
function runAIClassifier(issue) {
  const textToScan = `${issue.summary} ${issue.description}`.toLowerCase();
  let critical = false;
  let suggestedPriority = issue.originalPriority || "Medium";
  let explanation = "";

  // 1. Criticality scan
  const matchedCriticalKeywords = CRITICAL_KEYWORDS.filter(keyword => textToScan.includes(keyword));
  if (matchedCriticalKeywords.length > 0) {
    critical = true;
    suggestedPriority = "Highest";
    explanation = `System-Critical vulnerabilities detected: [${matchedCriticalKeywords.join(", ")}]. This directly impacts system availability, database connectivity, data security, or core checkout revenue streams. Suggested priority upgraded to Highest.`;
  } else {
    // 2. Scan for regular priority categories if not critical
    const matchedHighKeywords = HIGH_KEYWORDS.filter(keyword => textToScan.includes(keyword));
    const matchedLowKeywords = LOW_KEYWORDS.filter(keyword => textToScan.includes(keyword));

    if (matchedHighKeywords.length > 0 && matchedLowKeywords.length === 0) {
      suggestedPriority = "High";
      explanation = `Keywords reflecting high customer friction or service latency found: [${matchedHighKeywords.join(", ")}]. Suggested priority raised to High due to moderate blast radius.`;
    } else if (matchedLowKeywords.length > 0 && matchedHighKeywords.length === 0) {
      suggestedPriority = "Low";
      explanation = `UI tweaks, minor defects, or textual adjustments spotted: [${matchedLowKeywords.join(", ")}]. Suggested priority lowered to Low with no operational impact.`;
    } else {
      suggestedPriority = issue.originalPriority || "Medium";
      explanation = `No critical alerts or significant performance pain points identified in summary/description. AI suggests retaining standard JIRA priority.`;
    }
  }

  return {
    critical,
    suggestedPriority,
    explanation
  };
}

// Ingress Logger console simulation
function logToConsole(message, type = "process") {
  const consoleBody = document.getElementById("console-output");
  if (!consoleBody) return;
  
  const line = document.createElement("div");
  line.className = `console-line ${type}-line`;
  line.textContent = `[${new Date().toLocaleTimeString()}] ${message}`;
  consoleBody.appendChild(line);
  consoleBody.scrollTop = consoleBody.scrollHeight;
}

// Initialize Application UI
document.addEventListener("DOMContentLoaded", () => {
  // Navigation Tabs switching logic
  const navTabs = document.querySelectorAll(".nav-tab");
  const tabPanes = document.querySelectorAll(".tab-pane");

  navTabs.forEach(tab => {
    tab.addEventListener("click", () => {
      const targetPaneId = `pane-${tab.getAttribute("data-tab")}`;
      
      navTabs.forEach(t => t.classList.remove("active"));
      tabPanes.forEach(pane => pane.classList.remove("active"));

      tab.classList.add("active");
      const targetPane = document.getElementById(targetPaneId);
      if (targetPane) targetPane.classList.add("active");
    });
  });

  // Filter Action Elements
  document.getElementById("search-input").addEventListener("input", renderDashboard);
  document.getElementById("filter-critical").addEventListener("change", renderDashboard);
  document.getElementById("filter-mismatch").addEventListener("change", renderDashboard);
  document.getElementById("filter-component").addEventListener("change", renderDashboard);
  document.getElementById("btn-reset-filters").addEventListener("click", resetFilters);

  // Ingestion elements
  document.getElementById("preset-production-down").addEventListener("click", () => loadPreset("production-down"));
  document.getElementById("preset-backlog-triage").addEventListener("click", () => loadPreset("backlog-triage"));
  document.getElementById("preset-mixed-batch").addEventListener("click", () => loadPreset("mixed-batch"));
  document.getElementById("btn-parse-paste").addEventListener("click", parseTextareaInput);
  document.getElementById("btn-clear-paste").addEventListener("click", () => {
    document.getElementById("import-textarea").value = "";
    logToConsole("Paste area cleared.", "instruction");
  });
  document.getElementById("btn-mock-connect").addEventListener("click", simulateJiraCloudSync);

  // Sidebar Controls
  document.getElementById("sidebar-close").addEventListener("click", closeSidebar);
  document.getElementById("sidebar-overlay").addEventListener("click", closeSidebar);
  document.getElementById("btn-save-override").addEventListener("click", applyTriageOverrides);
  document.getElementById("btn-accept-ai").addEventListener("click", acceptAISuggestions);

  // Integration Tabs
  document.getElementById("btn-sync-jira").addEventListener("click", pushLocalOverridesToJira);
  document.getElementById("btn-export-json").addEventListener("click", downloadJSONDataset);
  document.getElementById("btn-export-csv").addEventListener("click", downloadCSVDataset);

  // Initial Data Load (loads Mixed Batch by default to show a beautiful UI immediately)
  loadPreset("mixed-batch", true);
});

// Load Preset Data Helper
function loadPreset(presetKey, silent = false) {
  const rawData = PRESETS[presetKey];
  if (!rawData) return;

  if (!silent) {
    logToConsole(`Ingesting dataset '${presetKey}'...`, "system");
    logToConsole("Triggering AI Classifications heuristics...", "process");
  }

  // Deep clone raw data and run AI analysis on each ticket
  const processedIssues = rawData.map(item => {
    const analysis = runAIClassifier(item);
    return {
      ...item,
      aiPriority: analysis.suggestedPriority,
      critical: analysis.critical,
      explanation: analysis.explanation,
      originalCritical: analysis.critical,
      userModified: false,
      userAccepted: false,
      currentPriority: analysis.suggestedPriority // Track current state (defaults to AI suggestion)
    };
  });

  issues = processedIssues;
  originalIssuesSnapshot = JSON.parse(JSON.stringify(processedIssues)); // Save snapshot
  syncQueue = []; // Reset sync overrides

  if (!silent) {
    setTimeout(() => {
      logToConsole(`Classification Complete! Loaded ${issues.length} JIRA issues.`, "success");
      logToConsole(`Stats: ${issues.filter(i=>i.critical).length} Critical tickets flagged, ${issues.filter(i=>i.currentPriority !== i.originalPriority).length} priority anomalies corrected.`, "success");
    }, 400);
  }

  renderDashboard();
}

// Ingestion Hub: Custom JSON paste handler
function parseTextareaInput() {
  const pasteContent = document.getElementById("import-textarea").value.trim();
  if (!pasteContent) {
    alert("Please paste some JSON JIRA issues array before processing.");
    return;
  }

  logToConsole("Initializing manual data intake parser...", "system");

  try {
    const rawData = JSON.parse(pasteContent);
    const issueArray = Array.isArray(rawData) ? rawData : [rawData];
    
    // Normalize data inputs
    const validatedData = issueArray.map((item, idx) => {
      return {
        key: item.key || `MANUAL-${100 + idx}`,
        summary: item.summary || "No Summary Provided",
        description: item.description || "No description loaded.",
        originalPriority: item.originalPriority || "Medium",
        component: item.component || "Billing & Checkout",
        status: item.status || "To Do"
      };
    });

    logToConsole(`Validating JSON formats... Found ${validatedData.length} records.`, "process");
    logToConsole("Initiating heuristic classification pipeline...", "process");

    const processed = validatedData.map(item => {
      const analysis = runAIClassifier(item);
      return {
        ...item,
        aiPriority: analysis.suggestedPriority,
        critical: analysis.critical,
        explanation: analysis.explanation,
        originalCritical: analysis.critical,
        userModified: false,
        userAccepted: false,
        currentPriority: analysis.suggestedPriority
      };
    });

    issues = [...issues, ...processed];
    originalIssuesSnapshot = JSON.parse(JSON.stringify(issues));

    logToConsole(`Custom Ingest Successful! Imported ${processed.length} new items into the backlog.`, "success");
    
    // Reset paste and jump to Kanban view
    document.getElementById("import-textarea").value = "";
    
    renderDashboard();

    // Trigger visual highlight to Kanban tab to show changes
    setTimeout(() => {
      document.querySelector('[data-tab="kanban"]').click();
    }, 600);

  } catch (err) {
    logToConsole(`Failed parsing JSON: ${err.message}`, "alert");
    alert(`Invalid JSON format. Please ensure your input is a valid JSON array or object.\n\nError: ${err.message}`);
  }
}

// Ingestion Hub: JIRA Mock API Sync Connector
function simulateJiraCloudSync() {
  const url = document.getElementById("jira-url").value.trim();
  const email = document.getElementById("jira-username").value.trim();
  const token = document.getElementById("jira-token").value.trim();

  if (!url || !email || !token) {
    alert("Please fill in JIRA site credentials to simulate connection.");
    return;
  }

  const connStatus = document.getElementById("jira-connection-status");
  connStatus.className = "connection-status syncing";
  connStatus.querySelector(".status-label").textContent = "Connecting to JIRA API...";
  
  logToConsole(`Connecting to Cloud Node: ${url}...`, "system");
  logToConsole(`Authenticating user ${email} with OAuth Token...`, "process");

  setTimeout(() => {
    logToConsole("Handshake established! Initializing schema download...", "process");
    
    setTimeout(() => {
      logToConsole("Downloading sprint scope issues (JQL: sprint in openSprints())...", "process");
      
      setTimeout(() => {
        logToConsole("Found 10 active tickets. Loading into Classifier pipeline...", "success");
        loadPreset("backlog-triage", true);
        
        connStatus.className = "connection-status connected";
        connStatus.querySelector(".status-label").textContent = "JIRA Cloud Connected";
        
        logToConsole("Sync Connection Active. Running real-time triage.", "success");
        
        // Jump to Kanban
        document.querySelector('[data-tab="kanban"]').click();
      }, 600);
    }, 600);
  }, 800);
}

// Reset Dashboard Filters
function resetFilters() {
  document.getElementById("search-input").value = "";
  document.getElementById("filter-critical").checked = false;
  document.getElementById("filter-mismatch").checked = false;
  document.getElementById("filter-component").value = "";
  renderDashboard();
}

// Core Dashboard Rendering Engine
function renderDashboard() {
  // Get filter settings
  const searchQuery = document.getElementById("search-input").value.trim().toLowerCase();
  const filterCriticalOnly = document.getElementById("filter-critical").checked;
  const filterMismatchOnly = document.getElementById("filter-mismatch").checked;
  const filterComponentVal = document.getElementById("filter-component").value;

  // Filter issues
  const filtered = issues.filter(issue => {
    // Search match
    const matchesSearch = 
      issue.key.toLowerCase().includes(searchQuery) ||
      issue.summary.toLowerCase().includes(searchQuery) ||
      issue.component.toLowerCase().includes(searchQuery);
    
    // Criticality filter
    const matchesCritical = !filterCriticalOnly || issue.critical;

    // Priority Mismatch filter (Jira priority != currentPriority)
    const matchesMismatch = !filterMismatchOnly || (issue.originalPriority !== issue.currentPriority);

    // Component filter
    const matchesComponent = !filterComponentVal || (issue.component === filterComponentVal);

    return matchesSearch && matchesCritical && matchesMismatch && matchesComponent;
  });

  // 1. Update KPIs
  updateKPIs();

  // 2. Render Kanban Columns
  renderKanbanBoard(filtered);

  // 3. Render Spreadsheet List Table
  renderListTable(filtered);

  // 4. Update Synchronization Badge counts
  updateSyncCounters();
}

// KPI Dashboard Card Updater
function updateKPIs() {
  const totalCount = issues.length;
  const criticalCount = issues.filter(i => i.critical).length;
  const discrepancyCount = issues.filter(i => i.originalPriority !== i.currentPriority).length;

  document.getElementById("kpi-total-count").textContent = totalCount;
  document.getElementById("kpi-critical-count").textContent = criticalCount;
  document.getElementById("kpi-discrepancy-count").textContent = discrepancyCount;

  // Percent critical
  const pctCritical = totalCount > 0 ? Math.round((criticalCount / totalCount) * 100) : 0;
  document.getElementById("kpi-critical-percent").textContent = `${pctCritical}% of Current Sprint`;

  // SLA buffer estimates based on critical items
  const slaBufferEl = document.getElementById("kpi-sla-buffer");
  const slaCaptionEl = document.getElementById("kpi-sla-caption");

  if (criticalCount > 0) {
    slaBufferEl.textContent = "1.8 hrs";
    slaBufferEl.className = "kpi-value text-critical";
    slaCaptionEl.textContent = "Risk: SLA Expiry Impending";
    slaCaptionEl.className = "kpi-trend negative";
  } else {
    slaBufferEl.textContent = "120 hrs";
    slaBufferEl.className = "kpi-value text-mint";
    slaCaptionEl.textContent = "All SLAs are safe";
    slaCaptionEl.className = "kpi-trend positive";
  }
}

// Sync Queue Counter
function updateSyncCounters() {
  // Items modified relative to original snapshot
  const modifiedCount = issues.filter(issue => {
    const original = originalIssuesSnapshot.find(o => o.key === issue.key);
    if (!original) return false;
    return original.currentPriority !== issue.currentPriority || original.critical !== issue.critical;
  }).length;

  document.getElementById("sync-stat-modified").textContent = modifiedCount;
  document.getElementById("sync-stat-critical").textContent = issues.filter(i => i.critical).length;
  
  const acceptedCount = issues.filter(i => i.userAccepted).length;
  document.getElementById("sync-stat-accepted").textContent = acceptedCount;

  // Global tab badge
  const pendingSyncBadge = document.getElementById("pending-sync-badge");
  if (modifiedCount > 0) {
    pendingSyncBadge.textContent = modifiedCount;
    pendingSyncBadge.classList.remove("hidden");
  } else {
    pendingSyncBadge.classList.add("hidden");
  }
}

// Kanban Render Workflow
function renderKanbanBoard(filteredIssues) {
  const priorities = ["Highest", "High", "Medium", "Low"];
  
  priorities.forEach(prio => {
    const columnContainer = document.getElementById(`cards-${prio.toLowerCase()}`);
    const countBadge = document.getElementById(`count-${prio.toLowerCase()}`);
    
    if (!columnContainer) return;
    
    // Clear out column
    columnContainer.innerHTML = "";

    // Filter issues in priority
    const colIssues = filteredIssues.filter(i => i.currentPriority === prio);
    countBadge.textContent = colIssues.length;

    colIssues.forEach(issue => {
      const card = document.createElement("div");
      card.className = "issue-card";
      card.draggable = true;
      card.dataset.issueKey = issue.key;
      
      // Bind drag event actions
      card.addEventListener("dragstart", handleDragStart);
      card.addEventListener("dragend", handleDragEnd);
      card.addEventListener("click", () => openDetailSidebar(issue));

      // Draw inside structure
      const isCritical = issue.critical;
      const isMismatch = issue.originalPriority !== issue.currentPriority;

      card.innerHTML = `
        <div class="card-header">
          <span class="card-key">${issue.key}</span>
          <span class="card-component">${issue.component}</span>
        </div>
        <div class="card-summary">${issue.summary}</div>
        <div class="card-footer">
          <div class="card-badges">
            ${isCritical ? '<span class="badge-critical">Critical</span>' : ''}
            ${isMismatch ? `<span class="badge-mismatch" title="JIRA priority: ${issue.originalPriority}">Anomaly</span>` : ''}
          </div>
          <div class="card-jira-prio">
            Jira: <span class="card-jira-prio-val">${issue.originalPriority}</span>
          </div>
        </div>
      `;

      columnContainer.appendChild(card);
    });
  });
}

// Spreadsheet Table Rendering
function renderListTable(filteredIssues) {
  const tbody = document.getElementById("issues-table-body");
  const emptyState = document.getElementById("empty-table-state");
  
  if (!tbody) return;
  tbody.innerHTML = "";

  if (filteredIssues.length === 0) {
    emptyState.classList.remove("hidden");
    return;
  }
  emptyState.classList.add("hidden");

  filteredIssues.forEach(issue => {
    const tr = document.createElement("tr");
    tr.dataset.issueKey = issue.key;
    tr.addEventListener("click", () => openDetailSidebar(issue));

    const isMismatch = issue.originalPriority !== issue.currentPriority;

    tr.innerHTML = `
      <td class="table-issue-key">${issue.key}</td>
      <td>
        <div class="table-summary" title="${issue.summary}">${issue.summary}</div>
      </td>
      <td>${issue.component}</td>
      <td><span class="badge-prio badge-prio-${issue.originalPriority.toLowerCase()}">${issue.originalPriority}</span></td>
      <td>
        <span class="badge-prio badge-prio-${issue.currentPriority.toLowerCase()}">${issue.currentPriority}</span>
        ${isMismatch ? `<span class="badge-mismatch ml-1" style="margin-left:5px" title="AI Suggests priority change from ${issue.originalPriority}">anomaly</span>` : ''}
      </td>
      <td>
        ${issue.critical ? '<span class="badge-critical">Critical</span>' : '<span class="table-status">Standard</span>'}
      </td>
      <td><span class="table-status">${issue.status}</span></td>
      <td style="text-align: center;">
        <button class="btn btn-secondary btn-row-action" title="View details">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" style="width:14px;height:14px"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"></path><circle cx="12" cy="12" r="3"></circle></svg>
        </button>
      </td>
    `;
    tbody.appendChild(tr);
  });
}

// Drag & Drop Handlers (HTML5 DND)
let draggedIssueKey = null;

function handleDragStart(e) {
  draggedIssueKey = this.dataset.issueKey;
  this.classList.add("dragging");
  
  // Set transfer details
  e.dataTransfer.effectAllowed = "move";
  e.dataTransfer.setData("text/plain", draggedIssueKey);

  // Style dropzones
  const columns = document.querySelectorAll(".column-cards");
  columns.forEach(col => col.classList.add("drag-hover-ready"));
}

function handleDragEnd() {
  this.classList.remove("dragging");
  draggedIssueKey = null;

  const columns = document.querySelectorAll(".column-cards");
  columns.forEach(col => {
    col.classList.remove("drag-over");
    col.classList.remove("drag-hover-ready");
  });
}

function allowDrop(e) {
  e.preventDefault();
  const columnCards = e.currentTarget;
  columnCards.classList.add("drag-over");
}

// Drops element on Kanban Board priority column
function drop(e, targetPriority) {
  e.preventDefault();
  
  const issueKey = e.dataTransfer.getData("text/plain");
  const issue = issues.find(i => i.key === issueKey);
  
  if (issue && issue.currentPriority !== targetPriority) {
    issue.currentPriority = targetPriority;
    issue.userModified = true;
    
    logToConsole(`Re-arranged card ${issueKey} to column [${targetPriority}] priority manually.`, "process");
    renderDashboard();
  }
}

// Detail Sidebar Open controls
function openDetailSidebar(issue) {
  selectedIssue = issue;
  
  document.getElementById("sidebar-key").textContent = issue.key;
  document.getElementById("sidebar-jira-status").textContent = issue.status;
  document.getElementById("sidebar-summary").textContent = issue.summary;
  document.getElementById("sidebar-description").textContent = issue.description || "No description provided.";
  
  document.getElementById("sidebar-component-pill").textContent = issue.component;
  document.getElementById("sidebar-original-priority-pill").textContent = `JIRA: ${issue.originalPriority}`;

  // AI evaluation fields
  const analysis = runAIClassifier(issue);
  document.getElementById("sidebar-ai-priority").textContent = analysis.suggestedPriority;
  document.getElementById("sidebar-ai-priority").className = `ai-badge-val badge-prio-${analysis.suggestedPriority.toLowerCase()}`;
  
  const aiCritBadge = document.getElementById("sidebar-ai-critical");
  if (analysis.critical) {
    aiCritBadge.textContent = "Critical Alert";
    aiCritBadge.className = "ai-badge-val badge-critical";
  } else {
    aiCritBadge.textContent = "Standard Severity";
    aiCritBadge.className = "ai-badge-val table-status";
  }

  document.getElementById("sidebar-ai-explanation").textContent = issue.explanation || analysis.explanation;

  // Form Fields presets
  document.getElementById("override-priority").value = issue.currentPriority;
  document.getElementById("override-critical").checked = issue.critical;

  // Open elements
  document.getElementById("issue-detail-sidebar").classList.add("open");
  document.getElementById("sidebar-overlay").classList.add("active");
}

// Detail Sidebar Close
function closeSidebar() {
  selectedIssue = null;
  document.getElementById("issue-detail-sidebar").classList.remove("open");
  document.getElementById("sidebar-overlay").classList.remove("active");
}

// Apply Manual UI Overrides inside details sheet
function applyTriageOverrides() {
  if (!selectedIssue) return;

  const targetPriority = document.getElementById("override-priority").value;
  const isCritical = document.getElementById("override-critical").checked;

  selectedIssue.currentPriority = targetPriority;
  selectedIssue.critical = isCritical;
  selectedIssue.userModified = true;

  logToConsole(`User applied triage modifications to ${selectedIssue.key}: Priority=${targetPriority}, Critical=${isCritical}`, "process");
  
  closeSidebar();
  renderDashboard();
}

// User explicitly accepts recommendation of AI suggestion
function acceptAISuggestions() {
  if (!selectedIssue) return;

  const analysis = runAIClassifier(selectedIssue);
  selectedIssue.currentPriority = analysis.suggestedPriority;
  selectedIssue.critical = analysis.critical;
  selectedIssue.userAccepted = true;
  selectedIssue.userModified = true;

  logToConsole(`User accepted AI recommended classification for ${selectedIssue.key} (Priority: ${analysis.suggestedPriority})`, "success");

  closeSidebar();
  renderDashboard();
}

// Simulates Pushing Local priority data overrides back to real JIRA instance
function pushLocalOverridesToJira() {
  const modifiedIssues = issues.filter(issue => {
    const original = originalIssuesSnapshot.find(o => o.key === issue.key);
    if (!original) return false;
    return original.currentPriority !== issue.currentPriority || original.critical !== issue.critical;
  });

  if (modifiedIssues.length === 0) {
    alert("No adjustments have been modified to sync back to JIRA.");
    return;
  }

  // Switch connection label
  const connStatus = document.getElementById("jira-connection-status");
  connStatus.className = "connection-status syncing";
  connStatus.querySelector(".status-label").textContent = "Syncing back to JIRA...";

  const progressWrapper = document.getElementById("sync-progress-wrapper");
  const progressFill = document.getElementById("sync-progress-fill");
  const progressStatus = document.getElementById("sync-progress-status");
  const progressPct = document.getElementById("sync-progress-pct");

  progressWrapper.classList.remove("hidden");
  progressFill.style.width = "0%";
  progressStatus.textContent = "Connecting to write-socket API...";
  progressPct.textContent = "0%";

  let pct = 0;
  const syncSteps = [
    { p: 15, msg: "Authorizing bulk-edit privileges..." },
    { p: 40, msg: `Patching Custom Fields [AI Priority] on ${modifiedIssues.length} issues...` },
    { p: 70, msg: "Injecting system-critical labels to incident tickets..." },
    { p: 90, msg: "Refreshing Jira database index pools..." },
    { p: 100, msg: "Synchronisation complete!" }
  ];

  const interval = setInterval(() => {
    pct += 5;
    if (pct > 100) pct = 100;

    progressFill.style.width = `${pct}%`;
    progressPct.textContent = `${pct}%`;

    const step = syncSteps.find(s => pct >= s.p - 5 && pct <= s.p);
    if (step) {
      progressStatus.textContent = step.msg;
    }

    if (pct === 100) {
      clearInterval(interval);
      
      // Update snapshot of original values to match current values
      originalIssuesSnapshot = JSON.parse(JSON.stringify(issues));
      
      setTimeout(() => {
        progressWrapper.classList.add("hidden");
        connStatus.className = "connection-status connected";
        connStatus.querySelector(".status-label").textContent = "JIRA Cloud Connected";
        
        logToConsole(`Successfully synced JIRA updates! Modified ${modifiedIssues.length} issues on the JIRA Cloud server.`, "success");
        alert(`Successfully synchronized ${modifiedIssues.length} ticket modifications to your JIRA Instance!`);
        
        renderDashboard();
      }, 500);
    }
  }, 100);
}

// Download Dataset as offline readable JSON payload
function downloadJSONDataset() {
  const exportData = issues.map(issue => {
    return {
      jiraKey: issue.key,
      summary: issue.summary,
      component: issue.component,
      originalPriority: issue.originalPriority,
      aiTriagePriority: issue.currentPriority,
      criticalTriageAlert: issue.critical,
      status: issue.status,
      categorizationExplanation: issue.explanation,
      userAcceptedRecommendation: issue.userAccepted,
      manualTriageOverride: issue.userModified
    };
  });

  const jsonStr = JSON.stringify(exportData, null, 2);
  const blob = new Blob([jsonStr], { type: "application/json" });
  const url = URL.createObjectURL(blob);
  
  const a = document.createElement("a");
  a.href = url;
  a.download = `jirapulse-triage-${new Date().toISOString().split('T')[0]}.json`;
  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
  URL.revokeObjectURL(url);
  
  logToConsole("Downloaded prioritized dataset in JSON format.", "success");
}

// Download Dataset as spreadsheet CSV sheet
function downloadCSVDataset() {
  let csvContent = "data:text/csv;charset=utf-8,";
  csvContent += "Jira Key,Summary,Component,Original Priority,Suggested Priority,Critical Severity,Sync Status\n";

  issues.forEach(issue => {
    const row = [
      issue.key,
      `"${issue.summary.replace(/"/g, '""')}"`,
      issue.component,
      issue.originalPriority,
      issue.currentPriority,
      issue.critical ? "CRITICAL" : "STANDARD",
      issue.status
    ].join(",");
    csvContent += row + "\n";
  });

  const encodedUri = encodeURI(csvContent);
  const a = document.createElement("a");
  a.setAttribute("href", encodedUri);
  a.setAttribute("download", `jirapulse-triage-${new Date().toISOString().split('T')[0]}.csv`);
  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
  
  logToConsole("Downloaded prioritized dataset in CSV format.", "success");
}
