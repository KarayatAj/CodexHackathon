/* 
  =========================================
  JiraPulse AI - Theme and Core Stylesheet
  =========================================
*/

/* Reset & Variables */
:root {
  --font-family: 'Plus Jakarta Sans', -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
  
  /* Color Palette (Premium Dark Mode) */
  --bg-color: #080c14;
  --bg-card: rgba(22, 31, 48, 0.7);
  --bg-card-hover: rgba(28, 40, 62, 0.85);
  --border-color: rgba(255, 255, 255, 0.08);
  --border-color-focus: rgba(99, 102, 241, 0.5);
  
  --text-main: #f3f4f6;
  --text-muted: #9ca3af;
  --text-darker: #4b5563;
  
  /* Priority Colors */
  --priority-highest: #f43f5e;
  --priority-highest-bg: rgba(244, 63, 94, 0.15);
  --priority-high: #f59e0b;
  --priority-high-bg: rgba(245, 158, 11, 0.15);
  --priority-medium: #0ea5e9;
  --priority-medium-bg: rgba(14, 165, 233, 0.15);
  --priority-low: #10b981;
  --priority-low-bg: rgba(16, 185, 129, 0.15);
  
  /* UI Accents */
  --accent: #6366f1;
  --accent-hover: #4f46e5;
  --accent-glow: rgba(99, 102, 241, 0.3);
  --critical-glow: rgba(239, 68, 68, 0.35);
  
  /* Transitions */
  --transition-fast: 0.2s cubic-bezier(0.4, 0, 0.2, 1);
  --transition-normal: 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  --shadow-lg: 0 10px 25px -5px rgba(0, 0, 0, 0.3), 0 8px 10px -6px rgba(0, 0, 0, 0.3);
  --shadow-glow: 0 0 20px rgba(99, 102, 241, 0.15);
}

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

body {
  background-color: var(--bg-color);
  color: var(--text-main);
  font-family: var(--font-family);
  overflow-x: hidden;
  min-height: 100vh;
  line-height: 1.5;
}

/* Background Ambient Orbs */
.glow-orb {
  position: fixed;
  border-radius: 50%;
  filter: blur(140px);
  z-index: -1;
  opacity: 0.35;
  pointer-events: none;
  animation: orb-float 20s infinite alternate ease-in-out;
}
.orb-1 {
  width: 400px;
  height: 400px;
  background: radial-gradient(circle, rgba(99, 102, 241, 0.6) 0%, rgba(99, 102, 241, 0) 70%);
  top: -10%;
  left: -5%;
}
.orb-2 {
  width: 500px;
  height: 500px;
  background: radial-gradient(circle, rgba(139, 92, 246, 0.5) 0%, rgba(139, 92, 246, 0) 70%);
  bottom: -15%;
  right: -5%;
  animation-delay: -5s;
}
.orb-3 {
  width: 350px;
  height: 350px;
  background: radial-gradient(circle, rgba(244, 63, 94, 0.3) 0%, rgba(244, 63, 94, 0) 70%);
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
  animation-delay: -10s;
}

@keyframes orb-float {
  0% { transform: translate(0, 0) scale(1); }
  50% { transform: translate(40px, -60px) scale(1.15); }
  100% { transform: translate(-30px, 30px) scale(0.9); }
}

/* Scrollbar Customization */
::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}
::-webkit-scrollbar-track {
  background: rgba(255, 255, 255, 0.02);
}
::-webkit-scrollbar-thumb {
  background: rgba(255, 255, 255, 0.15);
  border-radius: 4px;
}
::-webkit-scrollbar-thumb:hover {
  background: rgba(255, 255, 255, 0.25);
}

/* Glassmorphism Containers */
.glass-container {
  background: var(--bg-card);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);
  border: 1px solid var(--border-color);
  border-radius: 12px;
  box-shadow: var(--shadow-lg);
  transition: border-color var(--transition-normal), box-shadow var(--transition-normal);
}
.glass-container:hover {
  border-color: rgba(255, 255, 255, 0.12);
}

/* Layout Framework */
.app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  padding: 0 2rem;
  max-width: 1600px;
  margin: 0 auto;
  position: relative;
}

/* App Header styling */
.app-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1.5rem 0;
  border-bottom: 1px solid var(--border-color);
  margin-bottom: 2rem;
}

.header-logo {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.logo-pulse-icon {
  position: relative;
  width: 44px;
  height: 44px;
  background: linear-gradient(135deg, var(--accent), #8b5cf6);
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 0 15px rgba(99, 102, 241, 0.4);
}

.pulse-svg {
  width: 24px;
  height: 24px;
  color: white;
  stroke-dasharray: 60;
  animation: pulse-line 2.5s infinite linear;
}

.pulse-ring {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  border: 2px solid var(--accent);
  border-radius: 10px;
  animation: pulse-ring-expand 2s infinite ease-out;
}

@keyframes pulse-line {
  0% { stroke-dashoffset: 60; }
  100% { stroke-dashoffset: -60; }
}

@keyframes pulse-ring-expand {
  0% { transform: scale(1); opacity: 0.8; }
  100% { transform: scale(1.4); opacity: 0; }
}

.logo-text h1 {
  font-size: 1.35rem;
  font-weight: 800;
  letter-spacing: -0.5px;
  line-height: 1.2;
}

.accent-text {
  background: linear-gradient(135deg, #a5b4fc, #c084fc);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}

.version-badge {
  font-size: 0.72rem;
  font-weight: 500;
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid var(--border-color);
  padding: 0.15rem 0.5rem;
  border-radius: 6px;
  color: var(--text-muted);
}

/* Nav Tabs */
.nav-tabs {
  display: flex;
  background: rgba(255, 255, 255, 0.03);
  padding: 0.35rem;
  border-radius: 12px;
  border: 1px solid var(--border-color);
  gap: 0.25rem;
}

.nav-tab {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  background: transparent;
  border: none;
  color: var(--text-muted);
  font-family: var(--font-family);
  font-size: 0.9rem;
  font-weight: 600;
  padding: 0.6rem 1.1rem;
  border-radius: 8px;
  cursor: pointer;
  position: relative;
  transition: all var(--transition-fast);
}

.tab-icon {
  width: 16px;
  height: 16px;
}

.nav-tab:hover {
  color: var(--text-main);
  background: rgba(255, 255, 255, 0.04);
}

.nav-tab.active {
  color: white;
  background: var(--accent);
  box-shadow: 0 4px 12px rgba(99, 102, 241, 0.25);
}

.sync-badge {
  position: absolute;
  top: -4px;
  right: -4px;
  background: var(--priority-high);
  color: black;
  font-size: 0.65rem;
  font-weight: 800;
  width: 16px;
  height: 16px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  animation: badge-pulse 1.5s infinite alternate;
}

@keyframes badge-pulse {
  0% { transform: scale(1); }
  100% { transform: scale(1.15); }
}

.connection-status {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 0.9rem;
  background: rgba(255, 255, 255, 0.04);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  font-size: 0.82rem;
  font-weight: 600;
}

.status-dot {
  width: 8px;
  height: 8px;
  border-radius: 50%;
}

.connection-status.connected .status-dot {
  background-color: var(--priority-low);
  box-shadow: 0 0 8px var(--priority-low);
}

.connection-status.syncing .status-dot {
  background-color: var(--priority-high);
  box-shadow: 0 0 8px var(--priority-high);
  animation: blink 1s infinite alternate;
}

@keyframes blink {
  0% { opacity: 0.3; }
  100% { opacity: 1; }
}

/* KPI Statistics Panel */
.kpi-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
  gap: 1.5rem;
  margin-bottom: 2rem;
}

.kpi-card {
  display: flex;
  align-items: center;
  gap: 1.25rem;
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  border-radius: 12px;
  padding: 1.5rem;
  position: relative;
  overflow: hidden;
  box-shadow: var(--shadow-lg);
  transition: all var(--transition-normal);
}

.kpi-card:hover {
  transform: translateY(-2px);
  border-color: rgba(255, 255, 255, 0.15);
}

.kpi-icon {
  width: 48px;
  height: 48px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.kpi-icon svg {
  width: 24px;
  height: 24px;
}

.total-icon {
  background: rgba(99, 102, 241, 0.12);
  color: var(--accent);
}

.critical-icon {
  background: rgba(244, 63, 94, 0.15);
  color: var(--priority-highest);
}

.discrepancy-icon {
  background: rgba(245, 158, 11, 0.12);
  color: var(--priority-high);
}

.sla-icon {
  background: rgba(16, 185, 129, 0.12);
  color: var(--priority-low);
}

.kpi-details {
  display: flex;
  flex-direction: column;
}

.kpi-title {
  font-size: 0.78rem;
  color: var(--text-muted);
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.kpi-value {
  font-size: 1.8rem;
  font-weight: 800;
  line-height: 1.2;
  margin: 0.1rem 0;
}

.kpi-trend {
  font-size: 0.72rem;
  font-weight: 600;
}

.kpi-trend.positive { color: var(--priority-low); }
.kpi-trend.warning { color: var(--priority-high); }
.kpi-trend.negative { color: var(--priority-highest); }
.kpi-trend.neutral { color: var(--text-muted); }

/* Critical Card Special Pulse effect */
.kpi-card.critical-alert {
  border-color: rgba(244, 63, 94, 0.2);
}

.kpi-card.critical-alert:hover {
  border-color: rgba(244, 63, 94, 0.45);
  box-shadow: 0 0 25px rgba(244, 63, 94, 0.12);
}

.pulse-glow {
  position: absolute;
  top: -50%;
  left: -50%;
  width: 200%;
  height: 200%;
  background: radial-gradient(circle, rgba(244, 63, 94, 0.08) 0%, rgba(244, 63, 94, 0) 60%);
  pointer-events: none;
  animation: pulse-rotate 8s infinite linear;
}

@keyframes pulse-rotate {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

/* Filter Bar styles */
.filter-bar {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
  gap: 1.25rem;
  padding: 1.25rem;
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  border-radius: 12px;
  margin-bottom: 2rem;
  box-shadow: var(--shadow-lg);
}

.search-box {
  position: relative;
  flex-grow: 1;
  min-width: 300px;
  max-width: 500px;
}

.search-icon {
  position: absolute;
  left: 1rem;
  top: 50%;
  transform: translateY(-50%);
  width: 18px;
  height: 18px;
  color: var(--text-muted);
  pointer-events: none;
}

.search-box input {
  width: 100%;
  background: rgba(0, 0, 0, 0.2);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  padding: 0.7rem 1rem 0.7rem 2.8rem;
  color: white;
  font-family: var(--font-family);
  font-size: 0.9rem;
  transition: all var(--transition-fast);
}

.search-box input:focus {
  outline: none;
  border-color: var(--border-color-focus);
  box-shadow: 0 0 10px rgba(99, 102, 241, 0.15);
  background: rgba(0, 0, 0, 0.35);
}

.filter-controls {
  display: flex;
  align-items: center;
  flex-wrap: wrap;
  gap: 1.25rem;
}

/* Custom Toggle Control */
.toggle-control {
  display: flex;
  align-items: center;
  gap: 0.6rem;
  cursor: pointer;
  user-select: none;
}

.toggle-control input {
  display: none;
}

.toggle-slider {
  position: relative;
  width: 36px;
  height: 20px;
  background-color: var(--text-darker);
  border-radius: 20px;
  transition: all var(--transition-fast);
}

.toggle-slider::before {
  content: "";
  position: absolute;
  height: 14px;
  width: 14px;
  left: 3px;
  bottom: 3px;
  background-color: white;
  border-radius: 50%;
  transition: all var(--transition-fast);
}

.toggle-control input:checked + .toggle-slider {
  background-color: var(--accent);
}

.toggle-control input:checked + .toggle-slider::before {
  transform: translateX(16px);
}

.toggle-label {
  font-size: 0.85rem;
  color: var(--text-muted);
  font-weight: 600;
  transition: color var(--transition-fast);
}

.toggle-control:hover .toggle-label {
  color: var(--text-main);
}

/* Custom Select dropdowns */
.select-wrapper {
  position: relative;
}

.select-wrapper::after {
  content: "▾";
  font-size: 0.75rem;
  color: var(--text-muted);
  position: absolute;
  right: 1rem;
  top: 50%;
  transform: translateY(-50%);
  pointer-events: none;
}

select {
  appearance: none;
  background: rgba(0, 0, 0, 0.2);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  padding: 0.65rem 2.2rem 0.65rem 1rem;
  color: var(--text-main);
  font-family: var(--font-family);
  font-size: 0.85rem;
  font-weight: 600;
  cursor: pointer;
  transition: all var(--transition-fast);
}

select:focus {
  outline: none;
  border-color: var(--border-color-focus);
}

select option {
  background-color: var(--bg-color);
  color: white;
}

/* Buttons */
.btn {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  padding: 0.65rem 1.25rem;
  font-family: var(--font-family);
  font-size: 0.85rem;
  font-weight: 700;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  transition: all var(--transition-fast);
  white-space: nowrap;
}

.btn-primary {
  background: var(--accent);
  color: white;
}

.btn-primary:hover {
  background: var(--accent-hover);
  box-shadow: 0 4px 14px rgba(99, 102, 241, 0.35);
}

.btn-secondary {
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid var(--border-color);
  color: var(--text-main);
}

.btn-secondary:hover {
  background: rgba(255, 255, 255, 0.09);
  border-color: rgba(255, 255, 255, 0.2);
}

.btn-accent {
  background: linear-gradient(135deg, #6366f1, #a855f7);
  color: white;
  box-shadow: 0 4px 15px rgba(139, 92, 246, 0.2);
}

.btn-accent:hover {
  transform: translateY(-1px);
  box-shadow: 0 6px 18px rgba(139, 92, 246, 0.35);
}

.btn-icon {
  padding: 0.65rem;
  width: 38px;
  height: 38px;
}

.btn-full {
  width: 100%;
}

.icon-refresh {
  width: 16px;
  height: 16px;
}

/* Tab Panes switcher */
.tab-pane {
  display: none;
  animation: fade-in 0.4s ease-out;
}

.tab-pane.active {
  display: block;
}

@keyframes fade-in {
  from { opacity: 0; transform: translateY(8px); }
  to { opacity: 1; transform: translateY(0); }
}

/* =========================================
   KANBAN BOARD VIEW
   ========================================= */
.kanban-board {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 1.25rem;
  align-items: start;
  margin-bottom: 3rem;
}

@media (max-width: 1200px) {
  .kanban-board {
    grid-template-columns: repeat(2, 1fr);
  }
}

@media (max-width: 768px) {
  .kanban-board {
    grid-template-columns: 1fr;
  }
}

.kanban-column {
  background: rgba(14, 20, 32, 0.4);
  border: 1px solid var(--border-color);
  border-radius: 12px;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  max-height: 75vh;
}

.column-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-bottom: 0.75rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.05);
  margin-bottom: 1rem;
}

.header-title-container {
  display: flex;
  align-items: center;
  gap: 0.6rem;
}

.header-title-container h3 {
  font-size: 0.95rem;
  font-weight: 700;
}

.priority-indicator {
  width: 8px;
  height: 8px;
  border-radius: 50%;
}

.indicator-highest { background-color: var(--priority-highest); box-shadow: 0 0 6px var(--priority-highest); }
.indicator-high { background-color: var(--priority-high); box-shadow: 0 0 6px var(--priority-high); }
.indicator-medium { background-color: var(--priority-medium); box-shadow: 0 0 6px var(--priority-medium); }
.indicator-low { background-color: var(--priority-low); box-shadow: 0 0 6px var(--priority-low); }

.column-count {
  font-size: 0.75rem;
  font-weight: 700;
  background: rgba(255, 255, 255, 0.05);
  padding: 0.2rem 0.5rem;
  border-radius: 12px;
  border: 1px solid var(--border-color);
  color: var(--text-muted);
}

.column-cards {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
  overflow-y: auto;
  min-height: 250px;
  padding-right: 0.25rem;
  padding-bottom: 1rem;
}

/* Kanban Cards styling */
.issue-card {
  background: var(--bg-card);
  border: 1px solid var(--border-color);
  border-radius: 10px;
  padding: 1rem;
  cursor: grab;
  position: relative;
  transition: all var(--transition-normal);
  user-select: none;
  box-shadow: 0 4px 6px -1px rgba(0,0,0,0.1);
}

.issue-card:active {
  cursor: grabbing;
}

.issue-card:hover {
  transform: translateY(-2px);
  border-color: rgba(255, 255, 255, 0.16);
  box-shadow: 0 8px 16px -4px rgba(0, 0, 0, 0.35);
  background-color: var(--bg-card-hover);
}

/* Dragging state */
.issue-card.dragging {
  opacity: 0.4;
  transform: scale(0.96);
  border: 2px dashed var(--accent);
}

/* Column dragging hover outline */
.column-cards.drag-over {
  background: rgba(99, 102, 241, 0.05);
  border-radius: 8px;
  outline: 2px dashed rgba(99, 102, 241, 0.3);
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.5rem;
}

.card-key {
  font-size: 0.75rem;
  font-weight: 700;
  color: var(--accent);
}

.card-component {
  font-size: 0.68rem;
  font-weight: 600;
  background: rgba(255, 255, 255, 0.04);
  border: 1px solid var(--border-color);
  padding: 0.1rem 0.4rem;
  border-radius: 4px;
  color: var(--text-muted);
}

.card-summary {
  font-size: 0.85rem;
  font-weight: 600;
  line-height: 1.4;
  margin-bottom: 0.75rem;
  color: var(--text-main);
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.card-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-top: 1px solid rgba(255, 255, 255, 0.03);
  padding-top: 0.65rem;
  margin-top: 0.25rem;
}

.card-badges {
  display: flex;
  gap: 0.4rem;
  flex-wrap: wrap;
}

/* Critical badge styling */
.badge-critical {
  font-size: 0.65rem;
  font-weight: 800;
  background: rgba(239, 68, 68, 0.15);
  border: 1px solid rgba(239, 68, 68, 0.25);
  color: #ef4444;
  padding: 0.15rem 0.4rem;
  border-radius: 4px;
  text-transform: uppercase;
  letter-spacing: 0.2px;
  display: inline-flex;
  align-items: center;
  gap: 0.25rem;
  animation: card-alert-pulse 2s infinite alternate;
}

.badge-critical::before {
  content: "";
  width: 6px;
  height: 6px;
  background-color: #ef4444;
  border-radius: 50%;
  display: inline-block;
  box-shadow: 0 0 5px #ef4444;
}

@keyframes card-alert-pulse {
  0% { box-shadow: 0 0 4px rgba(239, 68, 68, 0); }
  100% { box-shadow: 0 0 10px rgba(239, 68, 68, 0.2); }
}

/* Discrepancy indicator badge */
.badge-mismatch {
  font-size: 0.65rem;
  font-weight: 700;
  background: rgba(245, 158, 11, 0.12);
  border: 1px solid rgba(245, 158, 11, 0.22);
  color: var(--priority-high);
  padding: 0.15rem 0.4rem;
  border-radius: 4px;
  display: inline-flex;
  align-items: center;
}

.card-jira-prio {
  font-size: 0.72rem;
  font-weight: 600;
  color: var(--text-muted);
}

.card-jira-prio-val {
  font-weight: 700;
}

/* Column Specific Header highlights */
.column-highest { border-top: 3px solid var(--priority-highest); }
.column-high { border-top: 3px solid var(--priority-high); }
.column-medium { border-top: 3px solid var(--priority-medium); }
.column-low { border-top: 3px solid var(--priority-low); }

/* =========================================
   SPREADSHEET TABLE LIST VIEW
   ========================================= */
.table-container {
  overflow-x: auto;
  margin-bottom: 3rem;
  border-radius: 12px;
}

.issues-table {
  width: 100%;
  border-collapse: collapse;
  text-align: left;
  font-size: 0.88rem;
}

.issues-table th {
  padding: 1rem 1.25rem;
  background: rgba(14, 20, 32, 0.6);
  color: var(--text-muted);
  font-weight: 700;
  border-bottom: 1px solid var(--border-color);
  font-size: 0.78rem;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.issues-table td {
  padding: 1rem 1.25rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.04);
  color: var(--text-main);
  vertical-align: middle;
}

.issues-table tbody tr {
  transition: background-color var(--transition-fast);
  cursor: pointer;
}

.issues-table tbody tr:hover {
  background-color: rgba(255, 255, 255, 0.02);
}

.table-issue-key {
  font-weight: 700;
  color: var(--accent);
}

.table-summary {
  font-weight: 600;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 320px;
}

/* Table Priority Badges */
.badge-prio {
  font-size: 0.72rem;
  font-weight: 700;
  padding: 0.25rem 0.6rem;
  border-radius: 6px;
  display: inline-flex;
  align-items: center;
  gap: 0.4rem;
  width: fit-content;
}

.badge-prio-highest {
  background-color: var(--priority-highest-bg);
  color: var(--priority-highest);
  border: 1px solid rgba(244, 63, 94, 0.2);
}
.badge-prio-high {
  background-color: var(--priority-high-bg);
  color: var(--priority-high);
  border: 1px solid rgba(245, 158, 11, 0.2);
}
.badge-prio-medium {
  background-color: var(--priority-medium-bg);
  color: var(--priority-medium);
  border: 1px solid rgba(14, 165, 233, 0.2);
}
.badge-prio-low {
  background-color: var(--priority-low-bg);
  color: var(--priority-low);
  border: 1px solid rgba(16, 185, 129, 0.2);
}

.table-status {
  font-size: 0.75rem;
  font-weight: 700;
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid var(--border-color);
  color: var(--text-muted);
  padding: 0.2rem 0.5rem;
  border-radius: 6px;
  width: fit-content;
}

.btn-row-action {
  padding: 0.4rem;
  width: 28px;
  height: 28px;
  border-radius: 6px;
}

.empty-table-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 4rem 2rem;
  color: var(--text-muted);
  gap: 1rem;
}

.empty-state-icon {
  width: 48px;
  height: 48px;
  color: var(--text-darker);
}

.empty-table-state.hidden {
  display: none;
}

/* =========================================
   INGESTION HUB VIEW
   ========================================= */
.ingest-grid {
  display: grid;
  grid-template-columns: 1.2fr 1fr;
  gap: 1.5rem;
  margin-bottom: 3rem;
}

@media (max-width: 1024px) {
  .ingest-grid {
    grid-template-columns: 1fr;
  }
}

.ingest-panel-left, .ingest-panel-right {
  padding: 2rem;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.panel-subtitle {
  font-size: 0.85rem;
  color: var(--text-muted);
  margin-top: -1rem;
}

.preset-selector-section h3, .manual-paste-section h3 {
  font-size: 0.95rem;
  font-weight: 700;
  margin-bottom: 0.75rem;
  border-left: 2px solid var(--accent);
  padding-left: 0.6rem;
}

.presets-grid {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
  margin-bottom: 1rem;
}

.preset-btn {
  background: rgba(255, 255, 255, 0.02);
  border: 1px solid var(--border-color);
  border-radius: 10px;
  padding: 1rem;
  text-align: left;
  cursor: pointer;
  font-family: var(--font-family);
  transition: all var(--transition-fast);
}

.preset-btn:hover {
  background: rgba(255, 255, 255, 0.05);
  border-color: rgba(255, 255, 255, 0.15);
  transform: translateX(4px);
}

.preset-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.25rem;
}

.preset-title {
  font-weight: 700;
  font-size: 0.9rem;
  color: white;
}

.preset-size {
  font-size: 0.7rem;
  font-weight: 700;
  background: rgba(99, 102, 241, 0.15);
  color: #a5b4fc;
  padding: 0.1rem 0.5rem;
  border-radius: 10px;
}

.preset-desc {
  font-size: 0.78rem;
  color: var(--text-muted);
  line-height: 1.3;
}

/* Custom raw textarea styles */
.section-help {
  font-size: 0.78rem;
  color: var(--text-muted);
  margin-bottom: 0.5rem;
}

#import-textarea {
  width: 100%;
  height: 160px;
  background: rgba(0, 0, 0, 0.25);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  padding: 0.75rem;
  color: #e5e7eb;
  font-family: 'Courier New', Courier, monospace;
  font-size: 0.8rem;
  resize: vertical;
  margin-bottom: 1rem;
}

#import-textarea:focus {
  outline: none;
  border-color: var(--border-color-focus);
}

.input-actions {
  display: flex;
  gap: 1rem;
}

/* Mock Connect Form */
.jira-mock-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.05);
  padding-bottom: 1.5rem;
  margin-bottom: 0.5rem;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
}

.form-group label {
  font-size: 0.78rem;
  font-weight: 700;
  color: var(--text-muted);
}

.form-group input {
  background: rgba(0, 0, 0, 0.2);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  padding: 0.65rem 0.9rem;
  color: white;
  font-family: var(--font-family);
  font-size: 0.88rem;
  transition: all var(--transition-fast);
}

.form-group input:focus {
  outline: none;
  border-color: var(--border-color-focus);
}

.form-row {
  display: flex;
  gap: 1rem;
}

.col-6 {
  flex: 1;
}

.btn-icon-svg {
  width: 16px;
  height: 16px;
}

/* Log Console Panel */
.console-box {
  background: #04070e;
  border: 1px solid rgba(255, 255, 255, 0.05);
  border-radius: 10px;
  font-family: 'Courier New', Courier, monospace;
  font-size: 0.78rem;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  height: 200px;
}

.console-header {
  background: rgba(255, 255, 255, 0.02);
  padding: 0.5rem 1rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.05);
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.console-title {
  color: var(--text-muted);
  font-weight: 700;
}

.console-dot-indicator {
  width: 8px;
  height: 8px;
  background-color: var(--priority-low);
  border-radius: 50%;
  box-shadow: 0 0 6px var(--priority-low);
  animation: console-pulse 1s infinite alternate;
}

@keyframes console-pulse {
  0% { opacity: 0.4; }
  100% { opacity: 1; }
}

.console-body {
  padding: 0.75rem 1rem;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 0.35rem;
  flex-grow: 1;
}

.console-line {
  line-height: 1.4;
  word-break: break-all;
}

.system-line { color: #8b5cf6; }
.instruction-line { color: var(--text-muted); }
.success-line { color: var(--priority-low); }
.process-line { color: #38bdf8; }
.alert-line { color: var(--priority-highest); }

/* =========================================
   SYNC & EXPORT TAB
   ========================================= */
.sync-grid {
  display: grid;
  grid-template-columns: 1.2fr 1fr;
  gap: 1.5rem;
  margin-bottom: 3rem;
}

@media (max-width: 1024px) {
  .sync-grid {
    grid-template-columns: 1fr;
  }
}

.sync-status-card, .export-options-card {
  padding: 2rem;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.sync-stats-box {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  background: rgba(0, 0, 0, 0.2);
  border: 1px solid var(--border-color);
  border-radius: 10px;
  padding: 1.25rem;
  text-align: center;
}

.sync-stat {
  display: flex;
  flex-direction: column;
  gap: 0.2rem;
}

.sync-stat:not(:last-child) {
  border-right: 1px solid rgba(255, 255, 255, 0.05);
}

.stat-num {
  font-size: 1.75rem;
  font-weight: 800;
}

.stat-lbl {
  font-size: 0.75rem;
  color: var(--text-muted);
  font-weight: 600;
}

.sync-actions-panel {
  background: rgba(255, 255, 255, 0.01);
  border: 1px solid rgba(255, 255, 255, 0.04);
  border-radius: 10px;
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.sync-actions-panel h3 {
  font-size: 1rem;
  font-weight: 700;
}

.sync-desc {
  font-size: 0.8rem;
  color: var(--text-muted);
  line-height: 1.4;
}

.sync-buttons {
  margin-top: 0.5rem;
}

/* Progress bar styling */
.progress-bar-wrapper {
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
}

.progress-info {
  display: flex;
  justify-content: space-between;
  font-size: 0.75rem;
  font-weight: 700;
}

.progress-bar-track {
  width: 100%;
  height: 6px;
  background: rgba(255, 255, 255, 0.05);
  border-radius: 3px;
  overflow: hidden;
}

.progress-bar-fill {
  height: 100%;
  background: linear-gradient(90deg, #6366f1, #a855f7);
  border-radius: 3px;
  transition: width 0.2s ease-out;
}

.export-options-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
}

@media (max-width: 580px) {
  .export-options-grid {
    grid-template-columns: 1fr;
  }
}

.export-card {
  background: rgba(0, 0, 0, 0.15);
  border: 1px solid var(--border-color);
  border-radius: 10px;
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
  align-items: center;
  text-align: center;
  transition: all var(--transition-fast);
}

.export-card:hover {
  border-color: rgba(255, 255, 255, 0.15);
  background: rgba(0, 0, 0, 0.25);
}

.export-format {
  font-size: 1.5rem;
  font-weight: 800;
  color: var(--accent);
  background: rgba(99, 102, 241, 0.1);
  width: 60px;
  height: 60px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.export-card-desc {
  font-size: 0.78rem;
  color: var(--text-muted);
  line-height: 1.4;
  flex-grow: 1;
}

/* =========================================
   RIGHT SIDEBAR DRAWERS
   ========================================= */
.detail-sidebar {
  position: fixed;
  top: 0;
  right: -420px;
  width: 400px;
  height: 100vh;
  background: #0d1321;
  border-left: 1px solid var(--border-color);
  box-shadow: -10px 0 30px rgba(0, 0, 0, 0.5);
  z-index: 100;
  display: flex;
  flex-direction: column;
  transition: right 0.35s cubic-bezier(0.16, 1, 0.3, 1);
  overflow-y: auto;
}

.detail-sidebar.open {
  right: 0;
}

.sidebar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1.5rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.05);
}

.sidebar-meta {
  display: flex;
  align-items: center;
  gap: 0.75rem;
}

.sidebar-issue-key {
  font-size: 0.9rem;
  font-weight: 800;
  color: var(--accent);
}

.status-badge {
  font-size: 0.72rem;
  font-weight: 700;
  background: rgba(255, 255, 255, 0.06);
  border: 1px solid var(--border-color);
  padding: 0.15rem 0.5rem;
  border-radius: 4px;
  color: var(--text-muted);
}

.sidebar-close-btn {
  background: transparent;
  border: none;
  color: var(--text-muted);
  cursor: pointer;
  padding: 0.25rem;
  border-radius: 6px;
  transition: all var(--transition-fast);
}

.sidebar-close-btn:hover {
  background: rgba(255, 255, 255, 0.05);
  color: white;
}

.sidebar-close-btn svg {
  width: 20px;
  height: 20px;
}

.sidebar-content {
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1.75rem;
}

.sidebar-section {
  display: flex;
  flex-direction: column;
  gap: 0.6rem;
}

.sidebar-issue-summary {
  font-size: 1.15rem;
  font-weight: 700;
  line-height: 1.35;
}

.sidebar-meta-pills {
  display: flex;
  gap: 0.5rem;
  margin-top: 0.25rem;
}

.meta-pill {
  font-size: 0.72rem;
  font-weight: 600;
  background: rgba(255, 255, 255, 0.04);
  border: 1px solid var(--border-color);
  color: var(--text-muted);
  padding: 0.15rem 0.5rem;
  border-radius: 4px;
}

.section-title {
  font-size: 0.78rem;
  font-weight: 700;
  color: var(--text-muted);
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.sidebar-issue-description {
  font-size: 0.85rem;
  color: #d1d5db;
  line-height: 1.5;
  background: rgba(0, 0, 0, 0.15);
  border: 1px solid var(--border-color);
  border-radius: 8px;
  padding: 0.75rem;
  white-space: pre-wrap;
}

/* AI Review Panel styles */
.ai-analyzer-box {
  background: rgba(99, 102, 241, 0.04);
  border: 1px solid rgba(99, 102, 241, 0.15);
  border-radius: 10px;
  padding: 1.25rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
  position: relative;
  box-shadow: 0 0 15px rgba(99, 102, 241, 0.03);
}

.ai-box-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.ai-box-title {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 0.82rem;
  font-weight: 800;
  color: #a5b4fc;
}

.ai-icon-pulse {
  width: 14px;
  height: 14px;
  color: #a5b4fc;
}

.ai-accuracy-badge {
  font-size: 0.68rem;
  font-weight: 700;
  color: #a5b4fc;
  background: rgba(99, 102, 241, 0.12);
  padding: 0.1rem 0.4rem;
  border-radius: 4px;
}

.ai-classification-badges {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 0.75rem;
}

.ai-badge-col {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.badge-lbl {
  font-size: 0.68rem;
  color: var(--text-muted);
  font-weight: 600;
}

.ai-badge-val {
  font-size: 0.85rem;
  font-weight: 800;
  padding: 0.4rem 0.75rem;
  border-radius: 6px;
  text-align: center;
  border: 1px solid rgba(255, 255, 255, 0.05);
}

.ai-rational-text {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.rational-title {
  font-size: 0.72rem;
  font-weight: 700;
  color: #a5b4fc;
}

#sidebar-ai-explanation {
  font-size: 0.8rem;
  line-height: 1.45;
  color: #a5b4fc;
  opacity: 0.9;
}

/* Sidebar override structures */
.override-toggle {
  background: rgba(255, 255, 255, 0.02);
  border: 1px solid var(--border-color);
  padding: 0.75rem;
  border-radius: 8px;
  width: 100%;
}

.toggle-label-bold {
  font-size: 0.82rem;
  font-weight: 700;
}

.sidebar-actions {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
  margin-top: 0.5rem;
}

/* Overlay Background cover */
.sidebar-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.6);
  backdrop-filter: blur(4px);
  z-index: 99;
  display: none;
  opacity: 0;
  transition: opacity 0.3s ease;
}

.sidebar-overlay.active {
  display: block;
  opacity: 1;
}

/* Helper Text Highlights */
.text-critical { color: var(--priority-highest); }
.text-amber { color: var(--priority-high); }
.text-sky { color: var(--priority-medium); }
.text-mint { color: var(--priority-low); }
