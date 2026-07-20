# 🤝 Contributing to LegendsTeamVN.BadmintonClub

First off, thank you for considering contributing to the LegendsTeamVN Badminton Club project! We welcome contributions from everyone.

*[Đọc bằng Tiếng Việt](CONTRIBUTING.VI.md)*

---

## 🛠️ How to Contribute

### 1. Fork & Clone
1. Fork the repository to your own GitHub account.
2. Clone your fork locally:
   ```bash
   git clone https://github.com/YOUR-USERNAME/LegendsTeamVN.BadmintonClub.git
   ```
3. Set up the local infrastructure and database as described in the [README.md](README.md).

### 2. Branching Strategy
Create a new branch for your work based on the `main` branch. Please use the following naming conventions:
- `feature/your-feature-name` (For new features)
- `bugfix/issue-description` (For bug fixes)
- `hotfix/urgent-fix` (For critical production bugs)
- `docs/update-readme` (For documentation changes)
- `chore/update-dependencies` (For maintenance tasks)

### 3. Making Changes & Coding Standards
Before you write code, please read our core architectural guidelines to ensure your code fits the project structure:
- 🏗️ [Clean Architecture & Workflow](docs/ARCHITECTURE.md)
- 🗄️ [Database Design Rules](docs/DATABASE.md)
- 🌐 [API Design Guidelines](docs/API_GUIDELINES.md)

**Important Rules:**
- Ensure your code passes all Architecture Tests (`tests/LegendsTeamVN.BadmintonClub.Architecture.Tests`).
- Never introduce external dependencies into the `Domain` layer.
- Format your code cleanly and remove unnecessary using statements.

### 4. Commit Message Conventions
We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification.
Format: `<type>(<scope>): <subject>`

**Examples:**
- `feat(users): add user profile update endpoint`
- `fix(auth): resolve token expiration issue`
- `docs: update API documentation in swagger`
- `refactor(core): optimize database queries`

### 5. Pull Requests (PR)
1. Push your branch to your forked repository.
2. Open a Pull Request against the `main` branch of the original repository.
3. Fill out the PR template completely. Clearly describe what you changed and why.
4. Wait for the CI/CD pipeline (GitHub Actions) to complete successfully.
5. Address any feedback from the Code Reviewers.

---

Once again, thank you for your time and effort! 🏸
