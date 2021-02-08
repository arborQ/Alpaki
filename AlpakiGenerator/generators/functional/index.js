var Generator = require('yeoman-generator');

module.exports = class extends Generator {
    constructor(args, opts) {
        super(args, opts);
    }

    async prompting() {
        this.answers = await this.prompt([
            {
                type: "input",
                name: "name",
                message: "What is the project name"
            }
        ]);
    }

    writing() {
        console.log({ ...this.answers });

        const answerOptions = {
            name: this.answers.name,
        };
        this.fs.copyTpl(
            this.templatePath('xunit.runner.json'),
            this.destinationPath(`test/${answerOptions.name}.Tests.Functional/xunit.runner.json`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('Tests.Functional.csproj'),
            this.destinationPath(`test/${answerOptions.name}.Tests.Functional/${answerOptions.name}.Tests.Functional.csproj`),
            { name: this.options.name, ...answerOptions }
        );

       this.fs.copyTpl(
            this.templatePath('Tests.Functional.csproj.user'),
            this.destinationPath(`test/${answerOptions.name}.Tests.Functional/${answerOptions.name}.Tests.Functional.csproj.user`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('AssemblyInfo.cs'),
            this.destinationPath(`test/${answerOptions.name}.Tests.Functional/AssemblyInfo.cs`)
        );

        this.fs.copyTpl(
            this.templatePath('TestCollections/TopicSubscriberSelfDestroyingTopicWrapper.cs'),
            this.destinationPath(`test/${answerOptions.name}.Tests.Functional/TestCollections/TopicSubscriberSelfDestroyingTopicWrapper.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('TestCollections/WebJobWithInternalApiFunctionalTests.cs'),
            this.destinationPath(`test/${answerOptions.name}.Tests.Functional/TestCollections/WebJobWithInternalApiFunctionalTests.cs`),
            { name: this.options.name, ...answerOptions }
        );
    }
};