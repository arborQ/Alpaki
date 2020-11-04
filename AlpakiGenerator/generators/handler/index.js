var Generator = require('yeoman-generator');

module.exports = class extends Generator {
    constructor(args, opts) {
        super(args, opts);
        // this.option("module", { type: String, required: true, default: "MotoLogic" });
        // this.option("name", { type: String, required: true });
    }
    // method1() {
    //     this.log('method 1 just ran');
    // }
    async prompting() {
        this.answers = await this.prompt([
            {
                type: "input",
                name: "name",
                message: "What is the name"
            },
            {
                type: 'checkbox',
                name: 'module',
                message: 'Select module',
                choices: [
                    { name: 'Dream', value: 'Dream' },
                    { name: 'Moto', value: 'Moto' },
                    { name: 'TimeSheet', value: 'TimeSheet' },
                ]
            },
            {
                type: 'checkbox',
                name: 'context',
                message: 'Select Db context',
                choices: [
                    { name: 'IDatabaseContext', value: 'IDatabaseContext' },
                    { name: 'IMotoDatabaseContext', value: 'IMotoDatabaseContext' },
                    { name: 'ITimeSheetDatabaseContext', value: 'ITimeSheetDatabaseContext' },
                ]
            },
            {
                type: "confirm",
                name: "unit",
                message: "Would you like to create unit tests?"
            }
        ]);

        // this.log("app name", answers.name);
        // this.log("cool feature", answers.cool);
    }
    // C:\Users\lukasz.wojcik\Alpaki\backend\Alpaki.MotoLogic\Handlers\
    writing() {
        console.log({ ...this.answers });

        const answerOptions = {
            name: this.answers.name,
            module : this.answers.module[0],
            context : this.answers.context[0]
        };
        this.fs.copyTpl(
            this.templatePath('handler'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${answerOptions.name}/${answerOptions.name}Handler.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('request'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${answerOptions.name}/${answerOptions.name}Request.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('response'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${answerOptions.name}/${answerOptions.name}Response.cs`),
            { name: this.options.name, ...answerOptions }
        );

        this.fs.copyTpl(
            this.templatePath('validator'),
            this.destinationPath(`Alpaki.${answerOptions.module}Logic/Handlers/${answerOptions.name}/${answerOptions.name}RequestValidator.cs`),
            { name: this.options.name, ...answerOptions }
        );

        if (this.answers.unit) {
            this.fs.copyTpl(
                this.templatePath('unit'),
                this.destinationPath(`Alpaki.${answerOptions.module}.UnitTests/Handlers/${answerOptions.name}HandlerTests.cs`),
                { ...answerOptions }
            );
        }
    }
};