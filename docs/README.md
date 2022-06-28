# Git commit message convention

**Follow these rules on squash commits**

```
feat: add hat wobble
^--^  ^------------^
|     |
|     +-> Summary in present tense.
|
+-------> Type: chore, docs, feat, fix, refactor, style, or test.
```

More Examples:

- `feat`: (new feature for the user, not a new feature for build script)
- `fix`: (bug fix for the user, not a fix to a build script)
- `docs`: (changes to the documentation)
- `style`: (formatting, missing semi colons, etc; no production code change)
- `refactor`: (refactoring production code, eg. renaming a variable)
- `test`: (adding missing tests, refactoring tests; no production code change)
- `chore`: (updating grunt tasks etc; no production code change)

References:

- https://www.conventionalcommits.org/
- https://seesparkbox.com/foundry/semantic_commit_messages
- http://karma-runner.github.io/1.0/dev/git-commit-msg.html

<br/>

# Git branches convention

**Follow Gitflow rules**

- Each branch should be related to an issue.
- Each branch should merge to main by pull request.

```
folder/branch-name
^--^  ^------------^
|     |
|     +-> branch name in kebab-case format.
|
+-------> Type: feature, hotfix, or release.
```

More Examples:

- `feature`: (new feature, dev-bug, requirement, or documentation)
- `hotfix`: (bug fix for released version)
- `release`: (released version from main branch)

References:

- https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow
